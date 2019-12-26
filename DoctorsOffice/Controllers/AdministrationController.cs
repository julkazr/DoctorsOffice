using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DoctorsOffice.Data;
using DoctorsOffice.ViewModels;
using DoctorsOffice.DbContexts;
using PagedList;
using DoctorsOffice.Translators;

namespace DoctorsOffice.Controllers
{
    public class AdministrationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public AdministrationController()
        {
        }

        public AdministrationController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: UserList
        public ActionResult Index(string sort, string searchByUserName, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            AdministrationListViewModel viewModel = new AdministrationListViewModel();
            //var sql = @"
            //SELECT AspNetUsers.UserName, AspNetRoles.Name As Role
            //FROM AspNetUsers 
            //LEFT JOIN AspNetUserRoles ON  AspNetUserRoles.UserId = AspNetUsers.Id 
            //LEFT JOIN AspNetRoles ON AspNetRoles.Id = AspNetUserRoles.RoleId";
            //var result = db.Database.SqlQuery<AdministrationViewModel>(sql).ToPagedList(pageNumber, pageSize);

            var usersQuery = UserManager.Users;

            if (!string.IsNullOrEmpty(searchByUserName))
            {
                usersQuery = usersQuery.Where(u => u.FullName.Contains(searchByUserName));
            }

            switch (sort)
            {
                case "name_desc":
                    usersQuery = usersQuery.OrderByDescending(u => u.FullName);
                    break;
                default:
                    usersQuery = usersQuery.OrderBy(p => p.FullName);
                    break;
            }

            AdministrationTranslator userTranslator = new AdministrationTranslator();
            viewModel.Users = usersQuery
                             .Select(userTranslator.ToViewModel)
                             .ToPagedList(pageNumber, pageSize);
            viewModel.CurrentSort = sort;
            viewModel.SortByUserName = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            viewModel.CurrentFilter = searchByUserName;

            return View(viewModel);
        }
    }
}