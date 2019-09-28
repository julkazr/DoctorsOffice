using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace DoctorsOffice.ViewModels
{
    public class DoctorsExaminationListViewModel
    {
        public IPagedList<DoctorsExaminationsViewModel> Examinations { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorName { get; set; }
        public int? DoctorID { get; set; }
        public string PatientFilter { get; set; }
        public string SortByDate { get; set; }
        public string SortByPatientName { get; set; }
        public string CurrentSort { get; set; }    
    }

    public class DoctorsExaminationsViewModel
    {
        public int ID { get; set; }
        public int? DoctorsID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExamDate { get; set; }
        public string Diagnose { get; set; }
        public string GetFullPatientName
        {
            get
            {
              return PatientFirstName + " " + PatientLastName;
            }
        }
        public string DiagnoseShort
        {
            get
            {
                return Diagnose.Length > 20 ? Diagnose.Substring(0, 19) : Diagnose;   
            }
        }
    }
}