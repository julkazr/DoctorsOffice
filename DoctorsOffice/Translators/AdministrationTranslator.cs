using DoctorsOffice.Data;
using DoctorsOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsOffice.Translators
{
    public class AdministrationTranslator
    {
        public AdministrationUserViewModel ToViewModel(ApplicationUser user)
        {
            AdministrationUserViewModel result = new AdministrationUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserEmail = user.Email,
                Role = " "
            };
            return result;
        }
    }
}