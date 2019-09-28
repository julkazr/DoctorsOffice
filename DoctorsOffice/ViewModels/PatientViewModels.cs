using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorsOffice.Data;
using PagedList;

namespace DoctorsOffice.ViewModels
{
    public class PatientViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string DoctorsFullName
        {
            get
            {
                return DoctorFirstName + " " + DoctorLastName;
            }
        }
    }

    public class PatientListViewModel
    {
        public IPagedList<PatientViewModel> Patients { get; set; }
        public List<Examination>  PatientExaminations{ get; set; }
        public string CurrentSort { get; set; }
        public string SortByname { get; set; }
        public string NameFilter { get; set; }
    }

    //same class is used as a view model for create and details view
    public class PatientEditViewModel: PatientViewModel
    {
        public string Address { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public BloodType? BloodType { get; set; }
        public string PatientSocSecurityNum { get; set; }
        public SelectList PersonalDoctorID { get; set; }
        public int SelectedDoctorID { get; set; }
    }

    public class PatientCreateViewModel: PatientEditViewModel
    {
        /*public PatientEditViewModel Patient { get; set; }*/
        public string HeightMetricUnit { get; set; }
        public string WeightMetricunit { get; set; }
    }

    public class PatientDeleteViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Alert { get; set; }
        public string PatientSocSecurityNum { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }

    public class PatientExaminationsViewModel
    {
        public int ID { get; set; }
        public int? PatientsID { get; set; }
        public string DoctorsFirstName { get; set; }
        public string DoctorsLastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime ExamDate { get; set; }
        public string Diagnose { get; set; }
        public string GetFullDoctorsName
        {
            get
            {
                return DoctorsFirstName + " " + DoctorsLastName;
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

    public class PatientExaminationsListViewModel
    {
        public IPagedList<PatientExaminationsViewModel> Examinations { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string CurrentSort { get; set; }
        public string SortByDate { get; set; }
        public string DoctorNameFilter { get; set; }
        public int? DoctorID { get; set; }
        public string GetPatientsName
        {
            get
            {
                return PatientFirstName + " " + PatientLastName;
            }
        }
    }
}