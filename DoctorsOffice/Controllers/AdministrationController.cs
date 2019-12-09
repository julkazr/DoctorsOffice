using DoctorsOffice.Data;
using DoctorsOffice.DbContexts;
using DoctorsOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorsOffice.Controllers
{
    public class AdministrationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administration
        public ActionResult Index(string sort, string searchByName, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            AdministrationViewModel viewModel = new AdministrationViewModel();

            IQueryable<ApplicationUser> usersQuery = db.Users
                                                       .Include(u => u.Roles);

            if (!string.IsNullOrEmpty(searchByName))
            {
                usersQuery = usersQuery
                    .Where(u => u.FullName.Contains(searchByName));

            }

            switch (sort)
            {
                case "name_desc":
                    usersQuery = usersQuery.OrderByDescending(u => u.FullName);
                    break;
                
                default:
                    usersQuery = usersQuery.OrderBy(u => u.FullName);
                    break;
            }

            return View(viewModel);
        }

        // GET: Administration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Administration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administration/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Administration/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Administration/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
