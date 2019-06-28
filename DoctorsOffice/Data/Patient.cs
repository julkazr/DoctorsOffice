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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfBirth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int? PersonalDoctorID { get; set; }

        public virtual Doctor PersonalDoctor { get; set; }
        public virtual ICollection<Examination> Examinations { get; set; }

        public BloodType? BloodType { get; set; }
    }
}