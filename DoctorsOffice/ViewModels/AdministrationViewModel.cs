using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsOffice.ViewModels
{
    public class AdministrationViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
    }
}