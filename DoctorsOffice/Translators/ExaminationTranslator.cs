using DoctorsOffice.Data;
using DoctorsOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsOffice.Translators
{
    public class ExaminationTranslator
    {
        public ExaminationViewModel ToViewList (Examination exam)
        {
            var result = new ExaminationViewModel()
            {
                ExaminationID = exam.ID,
                DoctorFirstName = exam.Doctor.FirstName,
                DoctorLastName = exam.Doctor.LastName,
                PatientFirstName = exam.Patient.FirstName,
                PatientLastName = exam.Patient.LastName,
                ExamDate = exam.DateOfVisit,
                Diagnose = exam.DiagnoseCode
            };
            return result;
        }

        public ExaminationCRUViewModel ToCRUViewModel (Examination exam)
        {
            var result = new ExaminationCRUViewModel
            {
                ExaminationID = exam.ID,
                DoctorFirstName = exam.Doctor.FirstName,
                DoctorLastName = exam.Doctor.LastName,
                PatientFirstName = exam.Patient.FirstName,
                PatientLastName = exam.Patient.LastName,
                ExamDate = exam.DateOfVisit,
                Diagnose = exam.DiagnoseCode,
                LabResult = exam.LabResults,
                ExamResult = exam.ExamResults
            };
            return result;
        }

        public ExaminationDeleteViewModel ToExamDeleteViewModel(Examination exam)
        {
            var result = new ExaminationDeleteViewModel
            {
                PatientFirstName = exam.Patient.FirstName,
                PatientLastName = exam.Patient.LastName,
                ExamDate = exam.DateOfVisit,
                Diagnose = exam.DiagnoseCode
            };
            return result;
        }

        public DoctorsExaminationsViewModel ToDoctorsExaminationsViewModel (Examination exam)
        {
            var result = new DoctorsExaminationsViewModel()
            {
                ID = exam.ID,
                DoctorsID = exam.DoctorID,
                PatientFirstName = exam.Patient.FirstName,
                PatientLastName = exam.Patient.LastName,
                ExamDate = exam.DateOfVisit,
                Diagnose = exam.DiagnoseCode
            };
            return result;
        }

        public PatientExaminationsViewModel ToPatientsExaminationsViewModel (Examination exam)
        {
            var result = new PatientExaminationsViewModel()
            {
                ID = exam.ID,
                PatientsID = exam.PatientID,
                DoctorsFirstName = exam.Doctor.FirstName,
                DoctorsLastName = exam.Doctor.LastName,
                ExamDate = exam.DateOfVisit,
                Diagnose = exam.DiagnoseCode
            };
            return result;
        }
    }
}