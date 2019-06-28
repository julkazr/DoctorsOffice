using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorsOffice.Data
{
    public enum FileType
    {
        Picture = 1, Photo
    }
    public class File
    {
        public int FileID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType Filetype { get; set; }
        public int DoctorID { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}