using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorsOffice.Data
{
    public class Patient: PersonInfo
    {
        public string PatientSocialSecurityNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int? PersonalDoctorID { get; set; }
        public BloodType? BloodType { get; set; }

        public virtual Doctor PersonalDoctor { get; set; }
        public virtual ICollection<Examination> Examinations { get; set; }  
    }
}