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
        public string DoctorsName { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public ImageEditViewModel Image { get; set; }
    }

    public class DoctorViewModel: DoctorBriefViewModel
    {
        public int DoctorID { get; set; }
    }
    public class ImageEditViewModel: ImageViewModel
    {

    }
}