using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;
using MageWarsWebSite.Web.App_LocalResources;
using MageWarsWebSite.Web.Areas.Admin.Models;
using MageWarsWebSite.Web.Controllers;
using MageWarsWebSite.Web.Models;

namespace MageWarsWebSite.Web.Areas.Admin.Controllers
{
    [Authorize()]
    public class CardsController : BaseController
    {
        #region Init
        private readonly IRepository _repo;
        public CardsController(IRepository repo)
        {
            _repo = repo;
            
        }
        #endregion Init


        // GET: Admin/Cards
        public async Task<ActionResult> Index(int page = 1, int count = 50)
        {
            var query = _repo.CardRepository.Cards;

            var model = await Task.Run(() => PagedData.GetPagedQuery(query, page, count).ToList());

            return View(model);
        }
        public ActionResult CardsNavigation()
        {
            var model = new NavigationViewModel()
            {
                ElementsCount = _repo.CardRepository.Cards.Count(),
                Action = "Index",
                Controller = "Cards",
                Area = "Admin", 
                ElementsPerPage = 50
            };
            return PartialView("NavigationPartial", model);
        }


        public async Task<ActionResult> Card(int id)
        {
            var model = await _repo.CardRepository.Cards
                .Include(c => c.CardTypes)
                .Include(c => c.SubTypes)
                .Include(c => c.Schools)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (model == null)
            {
                TempData["Error"] = string.Format(GlobalRes.CardNotFoundErrorFormat, id);
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<ActionResult> AddCard()
        {
            var model = new CardViewModel();

            await PopulateSelectListItems(model);

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCard(CardViewModel model, HttpPostedFileBase image)
        {
            await CheckModelErrorForAddCardMethod(model, image);

            //errors in model - return current View()
            if (!ModelState.IsValid)
            {
                await PopulateSelectListItems(model);

                return View(model);
            }

            await PopulateCardCollections(model);

            SaveUploadImageFile(model, image);

            var card = _repo.CardRepository.Add(model.Card);
            if (card == null)
            {
                TempData["Error"] = GlobalRes.CardAddError;
            }
            else
            {
                TempData["Success"] = string.Format(GlobalRes.CardAddSuccessFormat, card.Id);
            }

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> EditCard(int id)
        {
            var model = new CardViewModel
            {
                Card = await _repo.CardRepository.Cards.FirstOrDefaultAsync(c => c.Id == id)
            };

            if (model.Card == null)
            {
                TempData["Error"] = string.Format(GlobalRes.CardBadIdErrorFormat, id);
                return RedirectToAction("Index");
            }

            Session["CardId"] = model.Card.Id;
            Session["CardFileName"] = model.Card.FileName;

            await PopulateSelectListItems(model);

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCard(CardViewModel model, HttpPostedFileBase image)
        {
            if (model.Card.Id != (int) Session["CardId"])
            {
                TempData["Error"] = GlobalRes.CardChangedIdError;
                return RedirectToAction("Index");
            }
            await CheckModelErrorsForEditCardMethod(model);

            //errors in model
            if (!ModelState.IsValid)
            {
                await PopulateSelectListItems(model);

                return View(model);
            }

            //first, needs to delete all previous many-to-many relationships
            var entity = _repo.CardRepository.Cards
                .Include(c => c.CardTypes)
                .Include(c => c.SubTypes)
                .Include(c => c.Schools)
                .FirstOrDefault(c => c.Id == model.Card.Id);
            entity.SubTypes.Clear();
            entity.CardTypes.Clear();
            entity.Schools.Clear();
            _repo.CardRepository.SaveChanges();

            await PopulateCardCollections(model);

            SaveUploadImageFile(model, image);

            if (_repo.CardRepository.Update(model.Card))
            {
                TempData["Success"] = string.Format(GlobalRes.CardUpdateSuccessFormat, model.Card.Id);
            }
            else
            {
                TempData["Error"] = string.Format(GlobalRes.CardUpdateErrorFormat, model.Card.Id);
            }

            return RedirectToAction("Index");
        }
        

        public ActionResult DeleteCard(int id)
        {
            var card = _repo.CardRepository.GetById(id);
            if (card == null)
            {
                TempData["Error"] = string.Format(GlobalRes.CardNotFoundErrorFormat,id);
                return RedirectToAction("Index");
            }
            if(_repo.CardRepository.Delete(card))
            {
                TempData["Error"] = string.Format(GlobalRes.CardDeleteErrorFormat, id);
            }
            else
            {
                TempData["Success"] = string.Format(GlobalRes.CardDeleteSuccessFormat, id);
            }

            return RedirectToAction("Index");
        }


        #region Helpers Methods

        private async Task PopulateSelectListItems(CardViewModel model)
        {
            await _repo.SubTypeRepository.SubTypes.ForEachAsync(item =>
                model.SubTypesList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name }));

            await _repo.CardTypeRepository.CardTypes.ForEachAsync(item =>
                model.TypesList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name }));

            await _repo.SchoolRepository.Schools.ForEachAsync(item =>
                model.SchoolsList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name }));
        }

        private async Task PopulateCardCollections(CardViewModel model)
        {
            var card = model.Card;
            var tempList = new List<int>();
            model.SubTypes.ForEach(t => tempList.Add(Convert.ToInt32(t)));
            await _repo.SubTypeRepository.SubTypes
                .Join(tempList, t1 => t1.Id, t2 => t2, (t1, t2) => t1)
                .ForEachAsync(t => card.SubTypes.Add(t));

            tempList.Clear();
            model.Types.ForEach(t => tempList.Add(Convert.ToInt32(t)));
            await _repo.CardTypeRepository.CardTypes
                .Join(tempList, t1 => t1.Id, t2 => t2, (t1, t2) => t1)
                .ForEachAsync(t => card.CardTypes.Add(new CardType(t)));

            tempList.Clear();
            model.Schools.ForEach(t => tempList.Add(Convert.ToInt32(t)));
            await _repo.SchoolRepository.Schools
                .Join(tempList, t1 => t1.Id, t2 => t2, (t1, t2) => t1)
                .ForEachAsync(t => card.Schools.Add(t));
        }

        private void SaveUploadImageFile(CardViewModel model, HttpPostedFileBase image)
        {
            if (image != null)
            {
                var type = image.ContentType.Split('/')[1];
                var folder = Server.MapPath("/Content/Cards/");
                image.SaveAs($"{folder}{model.Card.FileName}.{type}");
                model.Card.FileType = type;
            }
        }

        private async Task CheckModelErrorForAddCardMethod(CardViewModel model, HttpPostedFileBase image)
        {
            if (image == null)
            {
                ModelState.AddModelError("", GlobalRes.ImageRequiredModelError);
            }
            if (await _repo.CardRepository.Cards.AnyAsync(c => c.FileName == model.Card.FileName))
            {
                ModelState.AddModelError("", GlobalRes.FileNameAlreadyExistModelError);
            }
            if (model.Types.Count == 0)
            {
                ModelState.AddModelError("", GlobalRes.TypeRequiredModelError);
            }
            if (model.SubTypes.Count == 0)
            {
                ModelState.AddModelError("", GlobalRes.TypeRequiredModelError);
            }
            if (model.Schools.Count == 0)
            {
                ModelState.AddModelError("", GlobalRes.TypeRequiredModelError);
            }
        }

        private async Task CheckModelErrorsForEditCardMethod(CardViewModel model)
        {
            var name = Session["CardFileName"] as string;
            if (await _repo.CardRepository.Cards.AnyAsync(c => c.FileName == model.Card.FileName && c.FileName != name))
            {
                ModelState.AddModelError("", GlobalRes.FileNameAlreadyExistModelError);
            }
            if (model.Types.Count == 0)
            {
                ModelState.AddModelError("", GlobalRes.TypeRequiredModelError);
            }
            if (model.SubTypes.Count == 0)
            {
                ModelState.AddModelError("", GlobalRes.TypeRequiredModelError);
            }
            if (model.Schools.Count == 0)
            {
                ModelState.AddModelError("", GlobalRes.TypeRequiredModelError);
            }
        }

        #endregion Helpers Methods
    }
}