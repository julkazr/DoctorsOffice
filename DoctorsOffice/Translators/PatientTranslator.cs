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
            
            PatientViewModel result;
            if(patient.PersonalDoctor != null)
            {
                result = new PatientViewModel()
                {
                    ID = patient.ID,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DoctorFirstName = patient.PersonalDoctor.FirstName,
                    DoctorLastName = patient.PersonalDoctor.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    Email = patient.Email
                };
            } else
            {
                result = new PatientViewModel()
                {
                    ID = patient.ID,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DoctorFirstName = "",
                    DoctorLastName = "",
                    PhoneNumber = patient.PhoneNumber,
                    Email = patient.Email
                };
            }
            return result;
        }

        public PatientEditViewModel ToPatientEditViewModel(Patient patient)
        {
            PatientEditViewModel result;
            if(patient.PersonalDoctor != null)
            {
                result = new PatientEditViewModel
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
            }
            else
            {
                result = new PatientEditViewModel
                {
                    ID = patient.ID,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    Email = patient.Email,
                    Address = patient.Address,
                    DateOfBirth = patient.DateOfBirth,
                    Height = patient.Height,
                    Weight = patient.Weight,
                    BloodType = patient.BloodType,
                    PatientSocSecurityNum = patient.PatientSocialSecurityNumber
                };
            }
            
            return result;
        }

        

        public Patient ToPatientDataModel (PatientCreateViewModel viewModel)
        {
            var result = new Patient
            {
                ID = viewModel.ID,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email,
                DateOfBirth = viewModel.DateOfBirth,
                Height = viewModel.Height,
                Weight = viewModel.Weight,
                BloodType = viewModel.BloodType,
                PatientSocialSecurityNumber = viewModel.PatientSocSecurityNum
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