using DoctorsOffice.Data;
using DoctorsOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsOffice.Translators
{
    public class PatientTranslator
    {
        public PatientViewModel ToViewModel(Patient patient)
        {
            var result = new PatientViewModel()
            {
                ID = patient.ID,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DoctorFirstName = patient.PersonalDoctor.FirstName,
                DoctorLastName = patient.PersonalDoctor.LastName,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email
            };
            return result;
        }

        public PatientEditViewModel ToPatientEditViewModel(Patient patient)
        {
            var result = new PatientEditViewModel
            {
                ID = patient.ID,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DoctorFirstName = patient.PersonalDoctor.FirstName,
                DoctorLastName = patient.PersonalDoctor.LastName,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                Address = patient.Address,
                DateOfBirth = patient.DateOfBirth,
                Height = patient.Height,
                Weight = patient.Weight,
                BloodType = patient.BloodType,
                PatientSocSecurityNum = patient.PatientSocialSecurityNumber
            };
            return result;
        }

        public Patient ToPatientDataModel (PatientCreateViewModel viewModel)
        {
            var result = new Patient
            {
                ID = viewModel.Patient.ID,
                FirstName = viewModel.Patient.FirstName,
                LastName = viewModel.Patient.LastName,
                Address = viewModel.Patient.Address,
                PhoneNumber = viewModel.Patient.PhoneNumber,
                Email = viewModel.Patient.Email,
                DateOfBirth = viewModel.Patient.DateOfBirth,
                Height = viewModel.Patient.Height,
                Weight = viewModel.Patient.Weight,
                BloodType = viewModel.Patient.BloodType,
                PatientSocialSecurityNumber = viewModel.Patient.PatientSocSecurityNum,
                PersonalDoctorID = viewModel.SelectedDoctorID
            };
            return result;
        }

        public PatientDeleteViewModel ToPatientDeleteViewModel (Patient patient)
        {
            var result = new PatientDeleteViewModel
            {
                ID = patient.ID,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                PatientSocSecurityNum = patient.PatientSocialSecurityNumber
    };
            return result;
        }
    }
}