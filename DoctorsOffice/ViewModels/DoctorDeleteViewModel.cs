using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsOffice.ViewModels
{
    public class DoctorDeleteViewModel
    {
        public int ID { get; set; }
        public string DoctorName { get; set; }
        public string Alert { get; set; }
        public ImageViewModel Image { get; set; }
    }
}