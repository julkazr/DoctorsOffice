using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsOffice.Domain.Models
{
    public class DoctorDomainModel
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

        public string Position { get; set; }
    

       

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
