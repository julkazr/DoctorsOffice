using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace DoctorsOffice.ViewModels
{
    public class AdministrationListViewModel
    {
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string SortByUserName { get; set; }
        public IPagedList<AdministrationUserViewModel> Users { get; set; }
    }

    public class UserRoleViewModel
    {
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class AdministrationUserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Role { get; set; }
    }
}