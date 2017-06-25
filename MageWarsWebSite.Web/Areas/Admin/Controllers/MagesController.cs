using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Web.App_LocalResources;
using MageWarsWebSite.Web.Areas.Admin.Models;
using MageWarsWebSite.Web.Controllers;
using MageWarsWebSite.Web.Models;
using static MageWarsWebSite.Web.Models.PagedData;

namespace MageWarsWebSite.Web.Areas.Admin.Controllers
{
    public class MagesController : BaseController
    {
        #region Init
        private readonly IRepository _repo;

        public MagesController(IRepository repo)
        {
            _repo = repo;
        }
        #endregion Init

        public async Task<ActionResult> Index(int page = 1, int count = 10)
        {
            var query = _repo.MageRepository.Mages;
            var model = await Task.Run(() => GetPagedQuery(query, page, count).ToList());

            return View(model);
        }
        public ActionResult MagesNavigation()
        {
            var model = new NavigationViewModel()
            {
                ElementsCount = _repo.MageRepository.Mages.Count(),
                Action = "Index",
                Controller = "Mages",
                Area = "Admin",
                ElementsPerPage = 10
            };
            return PartialView("NavigationPartial", model);
        }


        public async Task<ActionResult> Mage(int id)
        {
            var model = await _repo.MageRepository.Mages.FirstOrDefaultAsync(m => m.Id == id);

            if (model == null)
            {
                TempData["Error"] = string.Format(GlobalRes.MageNotFoundErrorFormat, id);
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<ActionResult> AddMage()
        {
            var model = new MageViewModel();

            await _repo.SchoolRepository.Schools
                .ForEachAsync(s => model.Schools.Add(new SelectListItem() {Value = s.Id.ToString(), Text = s.Name}));

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMage(MageViewModel model, HttpPostedFileBase descriptionImage,
            HttpPostedFileBase mageImage)
        {
            await CheckModelErrorForAddMageMethod(model, descriptionImage, mageImage);

            if (!ModelState.IsValid)
            {
                //TODO: check if SelectListItems populates
                return View(model);
            }

            SaveUploadImageFiles(model, descriptionImage, mageImage);

            var r = _repo.MageRepository.Add(model.Mage);
            if (r == null)
            {
                TempData["Error"] = GlobalRes.MageAddError;
            }
            else
            {
                TempData["Success"] = string.Format(GlobalRes.MageAddSuccessFormat, r.Id);
            }

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> EditMage(int id)
        {
            var model = new MageViewModel()
            {
                Mage = await _repo.MageRepository.Mages.FirstOrDefaultAsync(m => m.Id == id)
            };

            if (model.Mage == null)
            {
                TempData["Error"] = string.Format(GlobalRes.MageNotFoundErrorFormat, id);
                return RedirectToAction("Index");
            }
            Session["MageId"] = model.Mage.Id;
            Session["MageName"] = model.Mage.Name;
            Session["DescriptionFileName"] = model.Mage.DescriptionFileName;
            await _repo.SchoolRepository.Schools
                .ForEachAsync(s => model.Schools.Add(new SelectListItem() { Value = s.Id.ToString(), Text = s.Name }));

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMage(MageViewModel model, HttpPostedFileBase descriptionImage,
            HttpPostedFileBase mageImage)
        {
            if (model.Mage.Id != (int)Session["MageId"])
            {
                TempData["Error"] = GlobalRes.MageChangedIdError;
                return RedirectToAction("Index");
            }

            await CheckModelErrorForEditMageMethod(model);

            if (!ModelState.IsValid)
            {
                //TODO: check if SelectListItems populates
                return View(model);
            }

            SaveUploadImageFiles(model, descriptionImage, mageImage);

            var r = _repo.MageRepository.Update(model.Mage);
            if (!r)
            {
                TempData["Error"] = string.Format(GlobalRes.MageUpdateErrorFormat, model.Mage.Id);
            }
            else
            {
                TempData["Success"] = string.Format(GlobalRes.MageUpdateSuccessFormat, model.Mage.Id);
            }

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> DeleteMage(int id)
        {
            var mage = await _repo.MageRepository.Mages.FirstOrDefaultAsync(m => m.Id == id);
            if (mage == null)
            {
                TempData["Error"] = string.Format(GlobalRes.MageNotFoundErrorFormat, id);
                return RedirectToAction("Index");
            }
            var r = _repo.MageRepository.Delete(mage);
            if (!r)
            {
                TempData["Error"] = string.Format(GlobalRes.MageDeleteErrorFormat, id);
            }
            else
            {
                TempData["Success"] = string.Format(GlobalRes.MageDeleteSuccessFormat, id);
            }
            return RedirectToAction("index");
        }



        #region Helpers

        private void SaveUploadImageFiles(MageViewModel model, HttpPostedFileBase deskImage, HttpPostedFileBase heroImage)
        {
            if (deskImage != null)
            {
                var type = deskImage.ContentType.Split('/')[1];
                var folder = Server.MapPath("/Content/Cards/");
                deskImage.SaveAs($"{folder}{model.Mage.DescriptionFileName}.{type}");
                model.Mage.DescriptionFileType = type;
            }
            if (heroImage != null)
            {
                var type = heroImage.ContentType.Split('/')[1];
                var folder = Server.MapPath("/Content/Cards/");
                heroImage.SaveAs($"{folder}{model.Mage.HeroFileName}.{type}");
                model.Mage.HeroFileType = type;
            }
        }

        private async Task CheckModelErrorForAddMageMethod(MageViewModel model, HttpPostedFileBase descriptionImage,
            HttpPostedFileBase mageImage)
        {
            //model errors checking
            if (await _repo.MageRepository.Mages.AnyAsync(m => m.Name == model.Mage.Name))
            {
                ModelState.AddModelError("", GlobalRes.MageNameAlreadyExistModelError);
            }
            if (await _repo.MageRepository.Mages.AnyAsync(m => m.DescriptionFileName == model.Mage.DescriptionFileName))
            {
                ModelState.AddModelError("", GlobalRes.FileNameAlreadyExistModelError);
            }
            if (descriptionImage == null || mageImage == null)
            {
                ModelState.AddModelError("", GlobalRes.ImageRequiredModelError);
            }
        }

        private async Task CheckModelErrorForEditMageMethod(MageViewModel model)
        {
            //model errors checking
            var name = Session["MageName"] as string;
            var fName = Session["DescriptionFileName"] as string;
            
            if (await _repo.MageRepository.Mages.AnyAsync(m => m.Name == model.Mage.Name && m.Name != name))
            {
                ModelState.AddModelError("", GlobalRes.MageNameAlreadyExistModelError);
            }
            if (await _repo.MageRepository.Mages.AnyAsync(
                m => m.DescriptionFileName == model.Mage.DescriptionFileName && m.DescriptionFileName != fName))
            {
                ModelState.AddModelError("", GlobalRes.FileNameAlreadyExistModelError);
            }
        }

        #endregion Helpers
    }
}