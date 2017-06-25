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
    public class SubTypesController : Controller
    {
        #region Init
        private readonly IRepository _repo;

        public SubTypesController(IRepository repo)
        {
            _repo = repo;
        }
        #endregion Init

        // GET: Admin/CardTypes
        public async Task<ActionResult> Index(int page = 1, int count = 50)
        {
            var model = await Task.Run(() => GetPagedQuery(_repo.SubTypeRepository.SubTypes, page, count).ToList());
            return View(model);
        }
        public ActionResult SubTypesNavigation()
        {
            var model = new NavigationViewModel()
            {
                ElementsCount = _repo.SubTypeRepository.SubTypes.Count(),
                Action = "Index",
                Controller = "SubTypes",
                Area = "Admin",
                ElementsPerPage = 50
            };
            return PartialView("NavigationPartial", model);
        }


        public ActionResult AddSubType()
        {
            return View(new SubType());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddSubType(SubType model)
        {
            if (!ModelState.IsValid) return View(model);

            var r = _repo.SubTypeRepository.Add(model);
            if (r == null)
            {
                TempData["Error"] = GlobalRes.SubTypeAddError;
            }
            else
            {
                TempData["Success"] = string.Format(GlobalRes.SubTypeAddSuccessFormat, r.Id);
            }

            return RedirectToAction("Index");
        }


        public ActionResult AddSubTypesByParse()
        {
            return View((object)"");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddSubTypesByParse(string model)
        {
            if (!ModelState.IsValid) return View(model);


            var list = model.Trim().Split('\n');
            var b = true;
            foreach (var str in list)
            {
                var r = _repo.SubTypeRepository.Add(new SubType { Name = str.Trim() });
                if (r == null)
                    b = false;
            }

            if (b)
            {
                TempData["Success"] = GlobalRes.SubTypesAddSuccess;
            }
            else
            {
                TempData["Error"] = GlobalRes.SubTypesAddError;
            }

            return RedirectToAction("Index");
        }


        public ActionResult EditSubType(int id)
        {
            var model = _repo.SubTypeRepository.GetById(id);
            if (model == null)
            {
                TempData["Error"] = GlobalRes.SubTypeNotFoundErrorFormat;
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditSubType(SubType model)
        {
            if (!ModelState.IsValid) return View(model);

            var r = _repo.SubTypeRepository.Update(model);
            if (r)
            {
                TempData["Success"] = string.Format(GlobalRes.SubTypeUpdateSuccessFormat, model.Id);
            }
            else
            {
                TempData["Error"] = string.Format(GlobalRes.SubTypeUpdateErrorFormat, model.Id);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteSubType(int id)
        {
            var model = _repo.SubTypeRepository.GetById(id);
            if (model == null)
            {
                TempData["Error"] = string.Format(GlobalRes.SubTypeNotFoundErrorFormat, id);
                return RedirectToAction("Index");
            }
            var r = _repo.SubTypeRepository.Delete(model);

            if (r)
            {
                TempData["Success"] = string.Format(GlobalRes.SubTypeDeleteSuccessFormat, model.Id);
            }
            else
            {
                TempData["Error"] = string.Format(GlobalRes.SubTypeDeleteErrorFormat, model.Id);
            }

            return RedirectToAction("Index");

        }
    }
}