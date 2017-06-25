using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;
using MageWarsWebSite.Web.App_LocalResources;
using MageWarsWebSite.Web.Models;
using static MageWarsWebSite.Web.Models.PagedData;

namespace MageWarsWebSite.Web.Areas.Admin.Controllers
{
    public class SchoolsController : Controller
    {
        #region Init
        private readonly IRepository _repo;

        public SchoolsController(IRepository repo)
        {
            _repo = repo;
        }
        #endregion Init

        // GET: Admin/CardTypes
        public async Task<ActionResult> Index(int page = 1, int count = 10)
        {
            var model = await Task.Run(() => GetPagedQuery(_repo.SchoolRepository.Schools, page, count).ToList());
            return View(model);
        }
        public ActionResult SchoolsNavigation()
        {
            var model = new NavigationViewModel()
            {
                ElementsCount = _repo.SchoolRepository.Schools.Count(),
                Action = "Index",
                Controller = "Schools",
                Area = "Admin",
                ElementsPerPage = 10
            };
            return PartialView("NavigationPartial", model);
        }


        public ActionResult AddSchool()
        {
            return View(new School());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddSchool(School model)
        {
            if (!ModelState.IsValid) return View(model);

            var r = _repo.SchoolRepository.Add(model);
            if (r == null)
            {
                TempData["Error"] = GlobalRes.SchoolAddError;
            }
            else
            {
                TempData["Success"] = string.Format(GlobalRes.SchoolAddSuccessFormat, r.Id);
            }

            return RedirectToAction("Index");
        }


        public ActionResult AddSchoolsByParse()
        {
            return View((object)"");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddSchoolsByParse(string model)
        {
            if (!ModelState.IsValid) return View(model);


            var list = model.Trim().Split('\n');
            var b = true;
            foreach (var str in list)
            {
                var r = _repo.SchoolRepository.Add(new School { Name = str.Trim() });
                if (r == null)
                    b = false;
            }

            if (b)
            {
                TempData["Success"] = GlobalRes.SchoolsAddSuccess;
            }
            else
            {
                TempData["Error"] = GlobalRes.SchoolsAddError;
            }

            return RedirectToAction("Index");
        }


        public ActionResult EditSchool(int id)
        {
            var model = _repo.SchoolRepository.GetById(id);
            if (model != null) return View(model);

            TempData["Error"] = GlobalRes.SchoolNotFoundErrorFormat;
            return RedirectToAction("Index");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditSchool(School model)
        {
            if (!ModelState.IsValid) return View(model);

            var r = _repo.SchoolRepository.Update(model);
            if (r)
            {
                TempData["Success"] = string.Format(GlobalRes.SchoolUpdateSuccessFormat, model.Id);
            }
            else
            {
                TempData["Error"] = string.Format(GlobalRes.SchoolUpdateErrorFormat, model.Id);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteSchool(int id)
        {
            var model = _repo.SchoolRepository.GetById(id);
            if (model == null)
            {
                TempData["Error"] = string.Format(GlobalRes.SchoolNotFoundErrorFormat, id);
                return RedirectToAction("Index");
            }
            var r = _repo.SchoolRepository.Delete(model);

            if (r)
            {
                TempData["Success"] = string.Format(GlobalRes.SchoolDeleteSuccessFormat, model.Id);
            }
            else
            {
                TempData["Error"] = string.Format(GlobalRes.SchoolDeleteErrorFormat, model.Id);
            }

            return RedirectToAction("Index");

        }
    }
}