using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DoctorsOffice.Data;
using DoctorsOffice.DbContexts;
using DoctorsOffice.ViewModels;

namespace DoctorsOffice.Controllers
{
    public class DoctorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Doctors
        public ActionResult Index(string sort, string searchByName, string searchByPosition, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            DoctorListViewModel viewModel = new DoctorListViewModel();

            IQueryable<Doctor> doctorsQuery = db.Doctors;

            if (!string.IsNullOrEmpty(searchByName))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => d.FirstName.Contains(searchByName) || d.LastName.Contains(searchByName));
                
            }
            if (!string.IsNullOrEmpty(searchByPosition))
            {
                doctorsQuery = doctorsQuery.Where(d => d.Position.Contains(searchByPosition));
              
            }

            switch (sort)
            {
                case "name_desc":
                    doctorsQuery = doctorsQuery.OrderByDescending(d => d.LastName).ThenByDescending(d => d.FirstName);
                    break;
                case "position":
                    doctorsQuery = doctorsQuery.OrderBy(d => d.Position);
                    break;
                case "position_desc":
                    doctorsQuery = doctorsQuery.OrderByDescending(d => d.Position);
                    break;
                default:
                    doctorsQuery = doctorsQuery.OrderBy(d => d.LastName).ThenBy(d => d.FirstName);
                    break;
            }
            viewModel.Doctors = doctorsQuery
                    .Select(d => new DoctorBriefViewModel()
                    {
                        ID = d.ID,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        Email = d.Email,
                        Position = d.Position,
                        PhoneNumber = d.PhoneNumber
                    })
                    .ToPagedList(pageNumber, pageSize);

            viewModel.CurrentSort = sort;
            viewModel.SortByName = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            viewModel.SortByPosition = sort == "position" ? "position_desc" : "position";

            viewModel.NameFilter = searchByName;
            viewModel.PositionFilter = searchByPosition;

            return View(viewModel);
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            DoctorEditViewModel viewModel = new DoctorEditViewModel();
            viewModel.Doctor = new DoctorViewModel
            {
                DoctorID = doctor.ID,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Address = doctor.Address,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email,
                Position = doctor.Position
            };
            
            return View(viewModel);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            DoctorBriefViewModel viewModel = new DoctorBriefViewModel();
            return View(viewModel);
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Address,PhoneNumber,Email,Position,File")] Doctor doctor, HttpPostedFileBase upload)
        {
            DoctorBriefViewModel viewModel = new DoctorBriefViewModel();
            if (ModelState.IsValid)
            {
                //if (upload != null && upload.ContentLength > 0)
                //{

                //    var picture = new File()
                //    {
                //        FileName = System.IO.Path.GetFileName(upload.FileName),
                //        ContentType = upload.ContentType
                //    };
                //    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                //    {
                //        picture.Content = reader.ReadBytes(upload.ContentLength);
                //    }

                //    doctor.File = new File()
                //    {
                //        FileID = picture.FileID,
                //        DoctorID = doctor.ID,
                //        FileName = picture.FileName,
                //        ContentType = picture.ContentType,
                //        Content = picture.Content
                //    };
                //    db.Files.Add(picture);
                //    db.SaveChanges();
                //}

                db.Doctors.Add(doctor);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            Doctor doctor = db.Doctors.Find(id);
            //var doctorEditQuery = db.Doctors
            //                      .Include(d => d.File);
            //IQueryable<File> doctorPictureQuery = db.Files
            //                         .Include(f => f.Doctor)
            //                         .Where(f => f.DoctorID == id);
            DoctorEditViewModel viewModel = new DoctorEditViewModel();
            viewModel.Doctor = new DoctorViewModel
            {
                DoctorID = doctor.ID,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Address = doctor.Address,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email,
                Position = doctor.Position
            };

            //doctorPictureQuery = doctorPictureQuery.Where(f => f.DoctorID == id);

            //viewModel.Files = doctorPictureQuery.FirstOrDefault(f => f.DoctorID == id);




            return View(viewModel);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, DoctorEditViewModel viewModel)
        {
            //DoctorEditViewModel viewModel = new DoctorEditViewModel();
            Doctor doctorToUpdate = db.Doctors.Find(id);
            if (ModelState.IsValid)
            {

                doctorToUpdate.FirstName = viewModel.Doctor.FirstName;
                doctorToUpdate.LastName = viewModel.Doctor.LastName;
                doctorToUpdate.Address = viewModel.Doctor.Address;
                doctorToUpdate.PhoneNumber = viewModel.Doctor.PhoneNumber;
                doctorToUpdate.Email = viewModel.Doctor.Email;
                doctorToUpdate.Position = viewModel.Doctor.Position;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(viewModel);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            DoctorDeleteViewModel viewModel = new DoctorDeleteViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            
            viewModel.DoctorName = doctor.FirstName + " " + doctor.LastName;
            if (doctor == null)
            {
                return HttpNotFound();
            }
            viewModel.Alert = "Are you sure you want to delete informations for doctor " + viewModel.DoctorName +" ?";
            return View(viewModel);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");

            //return View();
        }

        public ActionResult ExaminationList (int? doctorId, string sort, string searchPatient, int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            
            Doctor doctor = db.Doctors.Find(doctorId);
            
            IQueryable<Examination> examinationQuery = db.Examinations
                                                       .Include(exam => exam.Doctor)
                                                       .Include(exam => exam.Patient);
           
            DoctorsExaminationListViewModel viewModel = new DoctorsExaminationListViewModel();

            if(!string.IsNullOrEmpty(searchPatient))
            {
                examinationQuery = examinationQuery
                                  .Where(exam => exam.Patient.FirstName.Contains(searchPatient) || 
                                                 exam.Patient.LastName.Contains(searchPatient));
            }

            examinationQuery = examinationQuery.Where(exam => exam.DoctorID == doctorId);
            var singleDoctorExaminationQuery = examinationQuery
                                .Select(exam => new DoctorsExaminationsViewModel()
                                {
                                    ID = exam.ID,
                                    DoctorsID = exam.DoctorID,
                                    PatientFirstName = exam.Patient.FirstName,
                                    PatientLastName = exam.Patient.LastName,
                                    ExamDate = exam.DateOfVisit,
                                    Diagnose = exam.DiagnoseCode
                                });

            switch (sort)
            {
                case "date_asc":
                    singleDoctorExaminationQuery = singleDoctorExaminationQuery.OrderBy(exam => exam.ExamDate);
                    break;
                case "Name":
                    singleDoctorExaminationQuery = singleDoctorExaminationQuery.OrderBy(exam => exam.PatientLastName).ThenBy(exam => exam.PatientFirstName);
                    break;
                case "name_desc":
                    singleDoctorExaminationQuery = singleDoctorExaminationQuery.OrderByDescending(exam => exam.PatientLastName).ThenByDescending(exam => exam.PatientFirstName);
                    break;
                default:
                    singleDoctorExaminationQuery = singleDoctorExaminationQuery.OrderByDescending(exam => exam.ExamDate);
                    break;
            }

            

            viewModel.Examinations = singleDoctorExaminationQuery
                                    .ToPagedList(pageNumber, pageSize);
            viewModel.SortByDate = string.IsNullOrEmpty(sort) ? "date_asc" : "";
            viewModel.SortByPatientName = sort == "Name" ? "name_desc" : "Name";
            viewModel.CurrentSort = sort;

            viewModel.PatientFilter = searchPatient;
            viewModel.DoctorName = doctor.FirstName + " " + doctor.LastName;
            viewModel.DoctorID = doctorId;
            

            return View("Examinations", viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
