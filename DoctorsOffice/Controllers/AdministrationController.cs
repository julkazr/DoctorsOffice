using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using DoctorsOffice.Data;
using DoctorsOffice.ViewModels;
using DoctorsOffice.DbContexts;
using PagedList;
using DoctorsOffice.Translators;
using System.Collections;

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
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sort, string currentFilter, string searchByUserName, int? page)
        {
            int intPage = 1;
            int intPageSize = 5;
            //int intTotalPageCount = 0;
            if (searchByUserName != null)
            {
                intPage = 1;
            }
            else
            {
                if (currentFilter != null)
                {
                    searchByUserName = currentFilter;
                    intPage = page ?? 1;
                }
                else
                {
                    searchByUserName = "";
                    intPage = page ?? 1;
                }
            }
            
           
            AdministrationListViewModel viewModel = new AdministrationListViewModel();
            int intSkip = (intPage - 1) * intPageSize;
            //intTotalPageCount = UserManager.Users
            //       .Where(x => x.UserName.Contains(searchByUserName))
            //       .Count();
            var usersQuery = UserManager.Users
                            .Where(x => x.UserName
                            .Contains(searchByUserName))
                            .OrderBy(x => x.UserName)
                            .Skip(intSkip)
                            .Take(intPageSize)
                            .ToList(); ;

            //if (!string.IsNullOrEmpty(searchByUserName))
            //{
            //    usersQuery = usersQuery.Where(u => u.FullName.Contains(searchByUserName));
            //}

            //switch (sort)
            //{
            //    case "name_desc":
            //        usersQuery = usersQuery.OrderByDescending(u => u.FullName);
            //        break;
            //    default:
            //        usersQuery = usersQuery.OrderBy(p => p.FullName);
            //        break;
            //}

            // AdministrationTranslator userTranslator = new AdministrationTranslator();
            //AdministrationListViewModel viewModel = new AdministrationListViewModel();
            foreach (var item in usersQuery)
            {
                AdministrationUserViewModel user = new AdministrationUserViewModel();
                user.UserId = item.Id;
                user.UserName = item.UserName;
                user.UserEmail = item.Email;
                user.Roles = UserManager.GetRoles(item.Id);
                //viewModel.Add(user);
            }
            viewModel.CurrentSort = sort;
            viewModel.SortByUserName = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            viewModel.CurrentFilter = searchByUserName;
            //foreach(var user in viewModel)
            //{
            //    viewModel.Roles = UserManager.GetRoles(user.UserId);
            //}
            //var _UserAsIPagedList =
            //        new StaticPagedList<>
            //        (
            //            col_UserViewMdel, intPage, intPageSize, intTotalPageCount
            //            );

            return View(viewModel);
        }

        #region Helpers
        //public class AdministrationTranslator
        //{
        //    public AdministrationUserViewModel ToViewModel(ApplicationUser user)
        //    {
        //        AdministrationUserViewModel result = new AdministrationUserViewModel
        //        {
        //            UserId = user.Id,
        //            UserName = user.FullName,
        //            UserEmail = user.Email,
        //            Roles = UserManager.GetRoles(user.UserId)
        //            };
        //        return result;
        //    }
        //}
        #endregion
    }
}