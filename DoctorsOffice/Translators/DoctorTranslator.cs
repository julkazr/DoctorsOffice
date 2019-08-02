using DoctorsOffice.Data;
using DoctorsOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsOffice.Translators
{
    public class DoctorTranslator
    {
        public DoctorBriefViewModel ToViewModel(Doctor doctor)
        {
            var result = new DoctorBriefViewModel()
            {
                ID = doctor.ID,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                Position = doctor.Position,
                PhoneNumber = doctor.PhoneNumber
            };
            return result;
        }
    }
}