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
    public class ExaminationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Examinations
        public ActionResult Index(string sort, string searchByPatientName, string searchByDoctorName, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ExaminationListViewModel viewModel = new ExaminationListViewModel();
            IQueryable<Examination> examinationsQuery = db.Examinations
                                                            .Include(exam => exam.Doctor)
                                                            .Include(exam => exam.Patient);

            if(!string.IsNullOrEmpty(searchByDoctorName))
            {
                examinationsQuery = examinationsQuery
                                    .Where(exam => exam.Doctor.FirstName.Contains(searchByDoctorName) || 
                                            exam.Doctor.LastName.Contains(searchByDoctorName));
            }
            if (!string.IsNullOrEmpty(searchByPatientName))
            {
                examinationsQuery = examinationsQuery
                                    .Where(exam => exam.Patient.FirstName.Contains(searchByPatientName) ||
                                            exam.Patient.LastName.Contains(searchByPatientName));
            }

            switch (sort)
            {
                case "date_asc":
                    examinationsQuery = examinationsQuery.OrderBy(exam => exam.DateOfVisit);
                    break;
                default:
                    examinationsQuery = examinationsQuery.OrderByDescending(exam => exam.DateOfVisit);
                    break;
            }

            ExaminationTranslator examTranslator = new ExaminationTranslator();
            viewModel.Examinations = examinationsQuery
                                        .Select(examTranslator.ToViewListModel)
                                        .ToPagedList(pageNumber, pageSize);

            viewModel.CurrentSort = sort;
            viewModel.SortByDate = string.IsNullOrEmpty(sort) ? "date_asc" : "";
            viewModel.DoctorNameFilter = searchByDoctorName;
            viewModel.PatientNameFilter = searchByPatientName;

            return View(viewModel);
        }

        // GET: Examinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examination examination = db.Examinations.SingleOrDefault(exam => exam.ID == id);
            if (examination == null)
            {
                return HttpNotFound();
            }

            ExaminationTranslator examinationDetailsTranslator = new ExaminationTranslator();
            ExaminationCRUViewModel viewModel = examinationDetailsTranslator.ToCRUViewModel(examination);

            return View(viewModel);
        }

        // GET: Examinations/Create
        public ActionResult Create()
        {
            ExaminationCRUViewModel viewModel = new ExaminationCRUViewModel();
            viewModel.PersonalDoctorID = new SelectList(db.Doctors, "ID", "FullName");
            viewModel.PatientID = new SelectList(db.Patients, "ID", "FullName");
            return View(viewModel);
        }

        // POST: Examinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExaminationCRUViewModel viewModel)
        {
            //var file = new file
            //{
            //    id = viewmodel.examfile.id,
            //    contenttype = viewmodel.examfile.fileupload.contenttype
            //};
            ExaminationTranslator examTranslator = new ExaminationTranslator();
            List<File> files = new List<File>();
            Examination exam = examTranslator.ToExaminationDataModel(viewModel, files);
            viewModel.PersonalDoctorID = new SelectList(db.Doctors, "ID", "FullName", exam.DoctorID);
            viewModel.PatientID = new SelectList(db.Patients, "ID", "FullName", exam.PatientID);
            if (ModelState.IsValid)
            {
                
                FileManipulation fileUploader = new FileManipulation();


                //foreach(file in files)
                //{
                //    fileUploader.FileUpload(viewModel.LabFile.FileUpload);
                //};
                var file = new File
                {
                    ID = viewModel.ExamFile.ID,
                    ContentType = viewModel.ExamFile.FileUpload.ContentType
                };
                fileUploader.FileUpload(file, viewModel.ExamFile.FileUpload);
                db.Files.Add(file);
                //foreach (file in files)
                //{
                //    fileUploader.FileUpload(viewModel.File.FileUpload);

                //};
                exam.DoctorID = viewModel.SelectedDoctorID;
                exam.PatientID = viewModel.SelectedPatientID;
                exam.Files = files;
                db.Examinations.Add(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Examinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examination examination = db.Examinations.Find(id);
            if (examination == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "FirstName", examination.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "FirstName", examination.PatientID);
            return View(examination);
        }

        // POST: Examinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateOfVisit,DoctorID,PatientID,DiagnoseCode,LabResults,ExamResults")] Examination examination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "FirstName", examination.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "FirstName", examination.PatientID);
            return View(examination);
        }

        // GET: Examinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examination examination = db.Examinations.SingleOrDefault(exam => exam.ID == id);
            if (examination == null)
            {
                return HttpNotFound();
            }
            ExaminationTranslator examDeleteTranslator = new ExaminationTranslator();
            ExaminationDeleteViewModel viewModel = examDeleteTranslator.ToExamDeleteViewModel(examination);

            return View(viewModel);
        }

        // POST: Examinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Examination examination = db.Examinations.SingleOrDefault(exam => exam.ID == id);
            db.Examinations.Remove(examination);
            db.SaveChanges();
            return RedirectToAction("Index");
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
