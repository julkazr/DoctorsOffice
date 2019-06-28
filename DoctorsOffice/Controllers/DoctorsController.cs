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
                                              //.Include(d => d.File);

            //viewModel.Doctors = db.Doctors
            //    .OrderBy(i => i.LastName)
            //    .Select(d => new DoctorBriefViewModel()
            //    {
            //        Id = d.ID,
            //        FullName = d.GetFullName,
            //        Email = d.Email,
            //        Position = d.Position,
            //        PhoneNumber = d.PhoneNumber
            //    })
            //    .ToPagedList(pageNumber, pageSize);
            
            //int count = viewModel.Doctors.Count();
            //decimal totalPages = ((decimal)count / (decimal)pageSize);
            //ViewBag.TotalPages = Math.Ceiling(totalPages);
            //ViewBag.OnePageOfDoctors = viewModel.Doctors;
            //ViewBag.PageNumber = pageNumber;

            if (!string.IsNullOrEmpty(searchByName))
            {
                doctorsQuery = doctorsQuery.Where(d => d.FirstName.Contains(searchByName) || d.LastName.Contains(searchByName));
                //viewModel.Doctors = db.Doctors
                //    .Where(d => d.FirstName.Contains(searchByName))
                //    .Select(d => new DoctorBriefViewModel()
                //    {
                //        Id = d.ID,
                //        FullName = d.GetFullName,
                //        Email = d.Email,
                //        Position = d.Position,
                //        PhoneNumber = d.PhoneNumber
                //    })
                //    .ToPagedList(pageNumber, pageSize);
            }
            if (!string.IsNullOrEmpty(searchByPosition))
            {
                doctorsQuery = doctorsQuery.Where(d => d.Position.Contains(searchByPosition));
                //viewModel.Doctors = viewModel.Doctors.Where(d => d.Position.Contains(searchByPosition)).ToPagedList(pageNumber, pageSize);
            }

            switch (sort)
            {
                case "name_desc":
                    doctorsQuery = doctorsQuery.OrderByDescending(d => d.LastName).ThenByDescending(d => d.FirstName);
                    //viewModel.Doctors = viewModel.Doctors.OrderByDescending(d => d.LastName).ToPagedList(pageNumber, pageSize);
                    break;
                case "position":
                    doctorsQuery = doctorsQuery.OrderBy(d => d.Position);
                    //viewModel.Doctors = viewModel.Doctors.OrderBy(d => d.Position).ToPagedList(pageNumber, pageSize);
                    break;
                case "position_desc":
                    doctorsQuery = doctorsQuery.OrderByDescending(d => d.Position);
                    //viewModel.Doctors = viewModel.Doctors.OrderByDescending(d => d.Position).ToPagedList(pageNumber, pageSize);
                    break;
                default:
                    doctorsQuery = doctorsQuery.OrderBy(d => d.LastName).ThenBy(d => d.FirstName);
                    //viewModel.Doctors = viewModel.Doctors.OrderBy(d => d.LastName).ToPagedList(pageNumber, pageSize);
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
            return View(doctor);
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
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Address,PhoneNumber,Email,Position")] Doctor doctor, HttpPostedFileBase upload)
        {
            DoctorBriefViewModel viewModel = new DoctorBriefViewModel();
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var picture = new File()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        Filetype = FileType.Picture,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        picture.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    doctor.File = new File();
                    
                }
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            DoctorBriefViewModel viewModel = new DoctorBriefViewModel();
            
            Doctor doctor = db.Doctors.Find(id);
            Doctor doctorEditQuery = db.Doctors.Include(d => d.File).SingleOrDefault(d => d.ID == id);
            File fileQuery = db.Files.SingleOrDefault(f => f.DoctorID == id);
            //viewModel.File = fileQuery.Select(d => new DoctorBriefViewModel()
            //{
            //    ID = d.ID,
            //    FirstName = d.FirstName,
            //    LastName = d.LastName,
            //    Address = d.Address,
            //    PhoneNumber = d.PhoneNumber,
            //    Email = d.Email,
            //    Position = d.Position,
            //    File = d.File.Content
            //});

            return View(viewModel);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Address,PhoneNumber,Email,Position")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            ViewBag.DoctorName = doctor.FirstName + " " + doctor.LastName;
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Are you sure you want to delete informations for doctor " + ViewBag.DoctorName +" ?";
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            //ViewBag.Message = "Are you sure you want to delete these informations?";
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
            var anotherQuery = examinationQuery
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
                    anotherQuery = anotherQuery.OrderBy(exam => exam.ExamDate);
                    break;
                case "Name":
                    anotherQuery = anotherQuery.OrderBy(exam => exam.PatientLastName).ThenBy(exam => exam.PatientFirstName);
                    break;
                case "name_desc":
                    anotherQuery = anotherQuery.OrderByDescending(exam => exam.PatientLastName).ThenByDescending(exam => exam.PatientFirstName);
                    break;
                default:
                    anotherQuery = anotherQuery.OrderByDescending(exam => exam.ExamDate);
                    break;
            }

            

            viewModel.Examinations = anotherQuery
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
