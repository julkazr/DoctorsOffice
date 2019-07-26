using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorsOffice.ViewModels
{
    public class DoctorCreateViewModel
    {
        public DoctorBriefViewModel Doctor { get; set; }
        public ImageViewModel Image { get; set; }
    }

    public class ImageViewModel
    {
        public int ID { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImgUpload { get; set; }
        public string ImgUrl { get; set; }
        public string DoctorName { get; set; }
    }
}