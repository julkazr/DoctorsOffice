using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DoctorsOffice.Data;

namespace DoctorsOffice.ViewModels
{
    public class DoctorEditViewModel
    {
        public DoctorViewModel Doctor { get; set; }
        public ImageViewModel Image { get; set; }
    }

    public class DoctorViewModel: DoctorBriefViewModel
    {
        public int DoctorID { get; set; }
    }
}