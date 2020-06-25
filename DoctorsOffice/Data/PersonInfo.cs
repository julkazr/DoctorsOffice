using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorsOffice.Data
{
    public abstract class PersonInfo
    {
        public int ID { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}