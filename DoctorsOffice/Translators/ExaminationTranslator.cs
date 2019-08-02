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
    }
}