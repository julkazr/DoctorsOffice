using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorsOffice.Data;

namespace DoctorsOffice.ViewModels
{
   
    public class ExaminationViewModel
    {
        public int ExaminationID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExamDate { get; set; }
        public string Diagnose { get; set; }

        public string GetFullDoctorsName
        {
            get
            {
                return DoctorFirstName + " " + DoctorLastName;
            }
        }
        public string GetFullPatientsName
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

    public class ExaminationListViewModel
    {
        public IPagedList<ExaminationViewModel> Examinations { get; set; }
        public string CurrentSort { get; set; }
        public string SortByDate { get; set; }
        public string DoctorNameFilter { get; set; }
        public string PatientNameFilter { get; set; }
    }

    public class ExaminationCRUViewModel: ExaminationViewModel
    {
        public SelectList ExamDoctorID { get; set; }
        public int? SelectedDoctorID { get; set; }
        public SelectList PatientID { get; set; }
        public int? SelectedPatientID { get; set; }
        public string LabResult { get; set; }
        public string ExamResult { get; set; }
        public FileViewModel File { get; set; }
        public ICollection<FileViewModel> Files { get; set; }
    }

    public class FileViewModel
    {
        public int ID { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase FileUpload { get; set; }
        public IEnumerable<HttpPostedFileBase> MultipleFileUpload { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public TypeOfFile FileType { get; set; }
    }

    public class ExaminationDeleteViewModel
    {
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExamDate { get; set; }
        public string Diagnose { get; set; }
        public string GetFullPatientsName
        {
            get
            {
                return PatientFirstName + " " + PatientLastName;
            }
        }
    }
    
}