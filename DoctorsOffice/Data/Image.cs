using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorsOffice.Data
{
    public class Image
    {
        public int ID { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImgUrl { get; set; }
    }
}