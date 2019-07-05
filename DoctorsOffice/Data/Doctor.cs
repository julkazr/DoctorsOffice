using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorsOffice.Data
{
    public class Doctor: PersonInfo
    {
        [Required]
        public string Position { get; set; }

        public virtual ICollection<Examination> Examinations { get; set; }
    }
}