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
using DoctorsOffice.Translators;
using DoctorsOffice.Helpers;

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
            var doctorTranslator = new DoctorTranslator();
            viewModel.Doctors = doctorsQuery
                    .Select(doctorTranslator.ToViewModel)
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
            Doctor doctor = db.Doctors.SingleOrDefault(d => d.ID == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            DoctorEditViewModel viewModel = new DoctorEditViewModel();
            DoctorTranslator doctorTranslator = new DoctorTranslator();
            viewModel.Doctor = doctorTranslator.ToDoctorViewModel(doctor);

            Image image = db.Images.Single(i => i.ID == doctor.ImageID);
            viewModel.Image = doctorTranslator.ToImageViewModel(image, doctor);

            return View(viewModel);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            DoctorCreateViewModel viewModel = new DoctorCreateViewModel();
            return View(viewModel);
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorCreateViewModel viewModel)
        {
            
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/png"
            };
            
            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    ID = viewModel.Image.ID,
                    ContentType = viewModel.Image.ImgUpload.ContentType
                };
                DoctorTranslator doctorDataTranslator = new DoctorTranslator();
                Doctor doctor = doctorDataTranslator.ToDoctorDataModel(viewModel, image);
                ImageManipulation imageUploadHelper = new ImageManipulation();
                if (viewModel.Image.ImgUpload != null && viewModel.Image.ImgUpload.ContentLength > 0)
                {
                    if (!validImageTypes.Contains(viewModel.Image.ImgUpload.ContentType))
                    {
                        ModelState.AddModelError("ImgUpload", "Please, choose either GIF, JPG, or PNG type of files.");
                    }
                    //upload with file-system
                    //var imgFileName = Guid.NewGuid().ToString() + GetExtension(viewModel.Image.ImgUpload.ContentType);
                    //var uploadDir = "~/Uploads";
                    //var imagePath = System.IO.Path.Combine(Server.MapPath(uploadDir), imgFileName);
                    //var imageUrl = System.IO.Path.Combine(uploadDir, imgFileName);
                    //viewModel.Image.ImgUpload.SaveAs(imagePath);
                    
                    imageUploadHelper.ImageUpload(doctor, image, viewModel.Image.ImgUpload);
                    //imageUploadHelper.ResizeImage(viewModel.Image.ImgUpload);-don't work yet!!!
                    db.Images.Add(image);
                }
                else
                {
                    imageUploadHelper.DefaultImage(doctor);
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
            var doctor = db.Doctors
                            .Include(d => d.Image)
                            .SingleOrDefault(d => d.ID == id);
            var doctorName = doctor.FirstName + " " + doctor.LastName;

            DoctorTranslator editDoctorTranslator = new DoctorTranslator();
            DoctorEditViewModel viewModel = new DoctorEditViewModel();
            viewModel.DoctorsName = doctorName;
            viewModel.Doctor = editDoctorTranslator.ToDoctorViewModel(doctor);


            Image image = db.Images.Single(i => i.ID == doctor.ImageID);
            viewModel.Image = editDoctorTranslator.ToImageViewModel(image, doctor);

            return View(viewModel);
        }

        public ActionResult GetImage(int id)
        {
            Image image = db.Images.Single(i => i.ID == id);
            return File(image.Content, image.ContentType);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, DoctorEditViewModel viewModel, HttpPostedFileBase upload)
        {

            Doctor doctorToUpdate = db.Doctors
                                        .Include(d => d.Image)
                                        .SingleOrDefault(d => d.ID == id);
            viewModel.Image.ID = doctorToUpdate.ImageID;
            var validImageTypes = new string[]
           {
                 "image/gif",
                 "image/jpeg",
                 "image/png"
           };
            ImageManipulation editImage = new ImageManipulation();
            if (ModelState.IsValid)
            {
                if (viewModel.Image.ImgUpload != null && viewModel.Image.ImgUpload.ContentLength > 0)
                {
                    if (!validImageTypes.Contains(viewModel.Image.ImgUpload.ContentType))
                    {
                        ModelState.AddModelError("ImgUpload", "Please, choose either GIF, JPG, or PNG type of files.");
                    }
                    if (viewModel.Image.ID == 1)
                    {
                        Image image = new Image();
                        editImage.ImageUpload(doctorToUpdate, image, viewModel.Image.ImgUpload);
                        //call for upload with file-system:
                        //EditImageUpload(viewModel.Image.ID, doctorToUpdate, image, viewModel);
                        db.Images.Add(image);
                    }
                    else
                    {
                        Image image = db.Images.Single(i => i.ID == doctorToUpdate.ImageID);
                        image.ID = viewModel.Image.ID;

                        editImage.ImageUpload(doctorToUpdate, image, viewModel.Image.ImgUpload);
                    }
                }
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

        //METHOD TO USE FOR UPLOAD IMAGE WITH FILE SYSTEM
        //private void EditImageUpload(int? id, Doctor doctorToUpdate, Image image, DoctorEditViewModel viewModel)
        //{
        //    ImageManipulation imageManipulationHelper = new ImageManipulation();
        //    var validImageTypes = new string[]
        //    {
        //         "image/gif",
        //         "image/jpeg",
        //         "image/png"
        //    };
        //    if (viewModel.Image.ImgUpload != null && viewModel.Image.ImgUpload.ContentLength > 0)
        //    {
        //        var imgFileName = Guid.NewGuid().ToString() + imageManipulationHelper.GetExtension(viewModel.Image.ImgUpload.ContentType);
                
        //        if (image.ImgUrl != "~/Img/doc-img-default.png") { 
        //            string fileToDelete = Server.MapPath(image.ImgUrl);
        //            if (System.IO.File.Exists(fileToDelete))
        //            {
        //                System.IO.File.Delete(fileToDelete);
        //            }
        //        }

        //        if (!validImageTypes.Contains(viewModel.Image.ImgUpload.ContentType))
        //        {
        //            ModelState.AddModelError("ImgUpload", "Please, choose either GIF, JPG, or PNG type of files.");
        //        }
               
        //        var uploadDir = "~/Uploads";
        //        var imagePath = System.IO.Path.Combine(Server.MapPath(uploadDir), imgFileName);
        //        var imageUrl = System.IO.Path.Combine(uploadDir, imgFileName);
        //        viewModel.Image.ImgUpload.SaveAs(imagePath);

        //        ImageResizer.ResizeSettings resizeSettings = new ImageResizer.ResizeSettings
        //        {
        //            Width = 200,
        //            Height = 200
        //        };
        //        ImageResizer.ImageBuilder.Current.Build(imagePath, imagePath, resizeSettings);

        //        viewModel.Image.ImgUrl = imageUrl;
        //        doctorToUpdate.Image = image;
        //        image.ImgUrl = viewModel.Image.ImgUrl;
        //    }
        //}

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            DoctorDeleteViewModel viewModel = new DoctorDeleteViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Single(d => d.ID == id);
            Image image = db.Images.Single(i => i.ID == doctor.ImageID);
            DoctorTranslator imageTranslator = new DoctorTranslator();
            viewModel.Image = imageTranslator.ToImageViewModel(image);

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
            Doctor doctor = db.Doctors.Single(d => d.ID == id); ;
            Image image = db.Images.Single(i => i.ID == doctor.ImageID);
            db.Doctors.Remove(doctor);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");

            //return View();
        }

        public ActionResult ExaminationList (int? doctorId, string sort, string searchPatient, int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            
            Doctor doctor = db.Doctors.SingleOrDefault(d => d.ID == doctorId);
            
            IQueryable<Examination> examinationQuery = db.Examinations
                                                       .Include(exam => exam.Doctor)
                                                       .Include(exam => exam.Patient);
           
            DoctorsExaminationListViewModel viewModel = new DoctorsExaminationListViewModel();
            ExaminationTranslator examTranslator = new ExaminationTranslator();
            if(!string.IsNullOrEmpty(searchPatient))
            {
                examinationQuery = examinationQuery
                                  .Where(exam => exam.Patient.FirstName.Contains(searchPatient) || 
                                                 exam.Patient.LastName.Contains(searchPatient));
            }

            examinationQuery = examinationQuery.Where(exam => exam.DoctorID == doctorId);
            var singleDoctorExaminationQuery = examinationQuery
                                .Select(examTranslator.ToDoctorsExaminationsViewModel);

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
