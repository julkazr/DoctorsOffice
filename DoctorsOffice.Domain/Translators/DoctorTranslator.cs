using DoctorsOffice.Data;
using DoctorsOffice.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DoctorsOffice.Translators
{
    public class DoctorTranslator
    {
        public DoctorDomainModel ToDomainModel(Doctor doctor)
        {
            var result = new DoctorDomainModel()
            {
                ID = doctor.ID,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                Position = doctor.Position,
                PhoneNumber = doctor.PhoneNumber
            };
            return result;
        }

        //public DoctorViewModel ToDoctorViewModel(Doctor doctor)
        //{
        //    var result = new DoctorViewModel
        //    {
        //        ID = doctor.ID,
        //        FirstName = doctor.FirstName,
        //        LastName = doctor.LastName,
        //        Address = doctor.Address,
        //        PhoneNumber = doctor.PhoneNumber,
        //        Email = doctor.Email,
        //        Position = doctor.Position,
        //        ImageID = doctor.ImageID
        //    };
        //    return result;
        //}

        //public ImageViewModel ToImageViewModel(File image, Doctor doctor)
        //{
        //    var result = new ImageViewModel
        //    {
        //        ID = image.ID,
        //        ImgUrl = image.FileUrl,
        //        DoctorName = doctor.FirstName + " " + doctor.LastName
        //    };
        //    return result;
        //}
        //public ImageViewModel ToImageViewModel(File image)
        //{
        //    var result = new ImageViewModel
        //    {
        //        ID = image.ID,
        //        ImgUrl = image.FileUrl
        //    };
        //    return result;
        //}

        //public Doctor ToDoctorDataModel(DoctorCreateViewModel viewModel, File image)
        //{
        //    var result = new Doctor
        //    {
        //        FirstName = viewModel.Doctor.FirstName,
        //        LastName = viewModel.Doctor.LastName,
        //        Address = viewModel.Doctor.Address,
        //        PhoneNumber = viewModel.Doctor.PhoneNumber,
        //        Email = viewModel.Doctor.Email,
        //        Position = viewModel.Doctor.Position,
        //        ImageID = image.ID,
        //        Image = image
        //    };
        //    return result;
        //}
    }
}