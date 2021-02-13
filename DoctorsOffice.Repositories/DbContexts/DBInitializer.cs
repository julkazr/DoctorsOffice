using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DoctorsOffice.Data;

namespace DoctorsOffice.DbContexts
{
    public class DBInitializer: DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        
        protected override void Seed(ApplicationDbContext context)
        {
            var image = new File
            {
                ID = 1,
                FileUrl = "~/Img/doc-img-default.png"
            };
            context.Files.Add(image);
            context.SaveChanges();

            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    Address = "Boston",
                    FirstName = "Leonard",
                    LastName = "Goldstein",
                    Email = "drface@doctorsoffice.mail",
                    PhoneNumber = "061498984",
                    Position = "plastic surgery",
                    ImageID = 1
                },
                new Doctor
                {
                    Address = "Boston",
                    FirstName = "Mathew",
                    LastName = "Volkov",
                    Email = "hands@doctorsoffice.mail",
                    PhoneNumber = "0611549375",
                    Position = "surgery",
                    ImageID = 1
                },
                new Doctor
                {
                    Address = "Boston",
                    FirstName = "Glenda",
                    LastName = "Sparks",
                    Email = "eyedr@doctorsoffice.mail",
                    PhoneNumber = "061432987",
                    Position = "oftalmologist",
                    ImageID = 1
                }
            };
            doctors.ForEach(d => context.Doctors.Add(d));
            context.SaveChanges();

            var patients = new List<Patient>
            {

                new Patient
                {
                    Address = "New York",
                    FirstName = "John",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("17-03-1965"),
                    Email = "jsmith@gmail.com", PhoneNumber = "0615557788",
                    Weight = 56,
                    Height = 172,
                    PersonalDoctorID = doctors.Single(d => d.LastName == "Sparks").ID
                },
                new Patient
                {
                    Address = "Philadelphia",
                    FirstName = "Miranda",
                    LastName = "Doe",
                    DateOfBirth = DateTime.Parse("23-5-1987"),
                    Email = "mdoe@yahoo.com",
                    PhoneNumber = "0642226698",
                    Weight = 71,
                    Height = 168,
                    PersonalDoctorID = doctors.Single(d => d.LastName == "Goldstein").ID,
                    BloodType = BloodType.A_PLUS
                },
                new Patient
                {
                    Address = "Alexandria",
                    FirstName = "Luiza",
                    LastName = "Garcia",
                    DateOfBirth = DateTime.Parse("03-07-1944"),
                    Email = "guiza@hotmail.com",
                    PhoneNumber = "0664679781",
                    Weight = 98,
                    Height = 195,
                    BloodType = BloodType.O_MINUS
                },
                new Patient
                {
                    Address = "Boston",
                    FirstName = "Anton",
                    LastName = "Mironov",
                    DateOfBirth = DateTime.Parse("15-12-2009"),
                    Email = "madrussian@gmail.com",
                    PhoneNumber = "0631887654",
                    Weight = 36,
                    Height = 144,
                    PersonalDoctorID = doctors.Single(d => d.LastName == "Volkov").ID,
                    BloodType = BloodType.AB_PLUS
                },
                new Patient
                {
                    Address = "New Haven",
                    FirstName = "Marko",
                    LastName = "Vali",
                    DateOfBirth = DateTime.Parse("12-09-2001"),
                    Email = "valim@gmail.com",
                    PhoneNumber = "0691784517",
                    Weight = 122,
                    Height = 201,
                    PersonalDoctorID = doctors.Single(d => d.LastName == "Volkov").ID
                }
            };
            patients.ForEach(p => context.Patients.Add(p));
            context.SaveChanges();

            var examinations = new List<Examination>
            {
                new Examination
                {
                    DateOfVisit = DateTime.Parse("05-12-2018"),
                    PatientID = patients.Single(p => p.LastName == "Garcia").ID,
                    DoctorID = doctors.Single(d => d.LastName == "Goldstein").ID,
                    DiagnoseCode = "nose reconstruction",
                    ExamResults = "Preoperation examination. All results ok."
                },
                new Examination
                {
                    DateOfVisit = DateTime.Parse("18-09-2018"),
                    PatientID = patients.Single(p => p.LastName == "Doe").ID,
                    DoctorID = doctors.Single(d => d.LastName == "Goldstein").ID,
                    DiagnoseCode = "mamoplastica",
                    ExamResults = "Postoperation examination. Patient feel a little bit of pain on the left side. US recomended."
                },
                new Examination
                {
                    DateOfVisit = DateTime.Parse("23-08-2018"),
                    PatientID = patients.Single(p => p.LastName == "Mironov").ID,
                    DoctorID = doctors.Single(d => d.LastName == "Volkov").ID,
                    DiagnoseCode = "apendicitis",
                    ExamResults = "Post op results in normal range."
                },
                new Examination
                {
                    DateOfVisit = DateTime.Parse("22-12-2018"),
                    PatientID = patients.Single(p => p.LastName == "Vali").ID,
                    DoctorID = doctors.Single(d => d.LastName == "Volkov").ID,
                    DiagnoseCode = "holecistitis",
                    ExamResults = "Lab resulsts for opeartion required."
                },
                new Examination
                {
                    DateOfVisit = DateTime.Parse("17-04-2019"),
                    PatientID = patients.Single(p => p.LastName == "Smith").ID,
                    DoctorID = doctors.Single(d => d.LastName == "Sparks").ID,
                    DiagnoseCode = "miopia",
                    ExamResults = "Dioptria -1.5, -1.75"
                },
                new Examination
                {
                    DateOfVisit = DateTime.Parse("17-01-2019"),
                    PatientID = patients.Single(p => p.LastName == "Garcia").ID,
                    DoctorID = doctors.Single(d => d.LastName == "Goldstein").ID,
                    DiagnoseCode = "nose reconstruction",
                    ExamResults = "Postoperation examination. All results ok. Patient feel fine."
                }
            };
            examinations.ForEach(e => context.Examinations.Add(e));
            context.SaveChanges();
        }
   
    }
}