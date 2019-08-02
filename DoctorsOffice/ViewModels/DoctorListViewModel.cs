using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DoctorsOffice.Data;
using PagedList;

namespace DoctorsOffice.ViewModels
{
    public class DoctorListViewModel
    {
        public IPagedList<DoctorBriefViewModel> Doctors { get; set; }
        public List<Examination> Examinations { get; set; }
        public string NameFilter { get; set; }
        public string PositionFilter { get; set; }
        public string SortByName { get; set; }
        public string SortByPosition { get; set; }
        public string CurrentSort { get; set; }

    }

    public class DoctorBriefViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int ImageID { get; set; }

        public string GetFullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}