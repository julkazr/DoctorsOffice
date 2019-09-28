using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorsOffice.Data
{

    public enum TypeOfFile
    {
        IMAGE,
        ULTRASOUND_RECORD,
        MRI_RECORD,
        TEXT_FILE,
        MISC
    }

    
    public class File
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public TypeOfFile TypeOfFile { get; set; }
        public string ContentType { get; set; }
        [DataType(DataType.Url)]
        public string FileUrl { get; set; }
    }
}