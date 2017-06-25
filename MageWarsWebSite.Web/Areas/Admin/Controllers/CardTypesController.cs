using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;
using MageWarsWebSite.Web.App_LocalResources;
using MageWarsWebSite.Web.Models;
using static MageWarsWebSite.Web.Models.PagedData;

namespace MageWarsWebSite.Web.Areas.Admin.Controllers
{
    public class CardTypesController : Controller
    {
        #region Init
        private readonly IRepository _repo;

        public CardTypesController(IRepository repo)
        {
            _repo = repo;
        }
        #endregion Init

        // GET: Admin/CardTypes
        public async Task<ActionResult> Index(int page = 1, int count = 5)
        {
            var model = await Task.Run( () => GetPagedQuery(_repo.CardTypeRepository.CardTypes, page, count).ToList());
            return View(model);
        }
        public ActionResult CardTypesNavigation()
        {
            var model = new NavigationViewModel()
            {
                ElementsCount = _repo.CardTypeRepository.CardTypes.Count(),
                Action = "Index",
                Controller = "CardTypes",
                Area = "Admin",
                ElementsPerPage = 5
            };
            return PartialView("NavigationPartial", model);
        }


        public ActionResult AddCardType()
        {
            return View(new CardType());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddCardType(CardType model)
        {
            if (!ModelState.IsValid) return View(model);

            var r = _repo.CardTypeRepository.Add(model);
            if (r == null)
            {
                TempData["Error"] = GlobalRes.CardTypeAddError;
            }
            else
            {
                TempData["Success"] = string.Format(GlobalRes.CardTypeAddSuccessFormat, r.Id);
            }

            return RedirectToAction("Index");
        }


        public ActionResult AddCardTypesByParse()
        {
            return View((object)"");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddCardTypesByParse(string model)
        {
            if (!ModelState.IsValid) return View(model);


            var list = model.Trim().Split('\n');
            var b = true;
            foreach (var str in list)
            {
                var r = _repo.CardTypeRepository.Add(new CardType(){Name = str.Trim()});
                if (r == null)
                    b = false;
            }

            if (b)
            {
                TempData["Success"] = GlobalRes.CardTypesAddSuccess;
            }
            else
            {
                TempData["Error"] = GlobalRes.CardTypesAddError;
            }

            return RedirectToAction("Index");
        }


        public ActionResult EditCardType(int id)
        {
            var model = _repo.CardTypeRepository.GetById(id);
            if (model == null)
            {
                TempData["Error"] = GlobalRes.CardTypeNotFoundErrorFormat;
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditCardType(CardType model)
        {
            if (!ModelState.IsValid) return View(model);

            var r = _repo.CardTypeRepository.Update(model);
            if (r)
            {
                TempData["Success"] = string.Format(GlobalRes.CardTypeUpdateSuccessFormat, model.Id);
            }
            else
            {
                TempData["Error"] = string.Format(GlobalRes.CardTypeUpdateErrorFormat, model.Id);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteCardType(int id)
        {
            var model = _repo.CardTypeRepository.GetById(id);
            if (model == null)
            {
                TempData["Error"] = string.Format(GlobalRes.CardTypeNotFoundErrorFormat, id);
                return RedirectToAction("Index");
            }
            var r = _repo.CardTypeRepository.Delete(model);

            if (r)
            {
                TempData["Success"] = string.Format(GlobalRes.CardTypeDeleteSuccessFormat, model.Id);
            }
            else
            {
                TempData["Error"] = string.Format(GlobalRes.CardTypeDeleteErrorFormat, model.Id);
            }

            return RedirectToAction("Index");

        }
    }
}