using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorsOffice.Data;
using DoctorsOffice.DbContexts;
using DoctorsOffice.Translators;
using DoctorsOffice.ViewModels;
using PagedList;

namespace DoctorsOffice.Controllers
{
    public class PatientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Patients
        public ActionResult Index(string sort, string searchByName, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            PatientListViewModel viewModel = new PatientListViewModel();
            IQueryable<Patient> patientsQuery = db.Patients
                                                  .Include(p => p.PersonalDoctor)
                                                  ;

            if (!string.IsNullOrEmpty(searchByName))
            {
                patientsQuery = patientsQuery.Where(p => p.FirstName.Contains(searchByName) || p.LastName.Contains(searchByName));
            }

            switch (sort)
            {
                case "name_desc":
                    patientsQuery = patientsQuery.OrderByDescending(p => p.LastName).ThenByDescending(p => p.FirstName);
                    break;
                default:
                    patientsQuery = patientsQuery.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
                    break;
            }

            PatientTranslator patientTranslator = new PatientTranslator();

            viewModel.Patients = patientsQuery
                                .Select(patientTranslator.ToViewModel)
                                .ToPagedList(pageNumber, pageSize);

            viewModel.CurrentSort = sort;
            viewModel.SortByname = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            viewModel.NameFilter = searchByName;

            return View(viewModel);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.SingleOrDefault(p => p.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            PatientTranslator patientDeatilsTranslator = new PatientTranslator();
            PatientEditViewModel viewModel = patientDeatilsTranslator.ToPatientEditViewModel(patient);

            return View(viewModel);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            PatientCreateViewModel viewModel = new PatientCreateViewModel();
            viewModel.PersonalDoctorID = new SelectList(db.Doctors, "ID", "FullName");
            viewModel.HeightMetricUnit = "cm";
            viewModel.WeightMetricunit = "kg";
            return View(viewModel);
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientCreateViewModel viewModel)
        {
            PatientTranslator patientDataTranslator = new PatientTranslator();
            Patient patient = patientDataTranslator.ToPatientDataModel(viewModel);
            viewModel.PersonalDoctorID = new SelectList(db.Doctors, "ID", "FullName", patient.PersonalDoctorID);
            
            if (ModelState.IsValid)
            {
                
                    patient.PersonalDoctorID = viewModel.SelectedDoctorID;
                
                
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(viewModel);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients
                                .Include(p => p.PersonalDoctor)
                                .SingleOrDefault(p => p.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            PatientTranslator patientEditTranslator = new PatientTranslator();
            PatientEditViewModel viewModel = new PatientEditViewModel();
            viewModel = patientEditTranslator.ToPatientEditViewModel(patient);
            viewModel.PersonalDoctorID = new SelectList(db.Doctors, "ID", "FullName", patient.PersonalDoctorID);
            return View(viewModel);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, PatientEditViewModel viewModel)
        {
            Patient patientToUpdate = db.Patients.SingleOrDefault(p => p.ID == id);
            if (ModelState.IsValid)
            {
                patientToUpdate.FirstName = viewModel.FirstName;
                patientToUpdate.LastName = viewModel.LastName;
                patientToUpdate.PatientSocialSecurityNumber = viewModel.PatientSocSecurityNum;
                patientToUpdate.PersonalDoctorID = viewModel.SelectedDoctorID;
                patientToUpdate.Address = viewModel.Address;
                patientToUpdate.PhoneNumber = viewModel.PhoneNumber;
                patientToUpdate.Email = viewModel.Email;
                patientToUpdate.DateOfBirth = viewModel.DateOfBirth;
                patientToUpdate.BloodType = viewModel.BloodType;
                patientToUpdate.Height = viewModel.Height;
                patientToUpdate.Weight = viewModel.Weight;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            viewModel.PersonalDoctorID = new SelectList(db.Doctors, "ID", "FirstName", patientToUpdate.PersonalDoctorID);
            return View(viewModel);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            PatientDeleteViewModel viewModel = new PatientDeleteViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Single(p => p.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            PatientTranslator patientDeleteTranslator = new PatientTranslator();
            viewModel = patientDeleteTranslator.ToPatientDeleteViewModel(patient);
            viewModel.Alert = "Are you sure you want to delete informations for patient " + viewModel.FullName + " ?";

            return View(viewModel);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Single(p => p.ID == id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExaminationList (int? patientId, string sort, string searchByDoctorName, int? page )
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);

            Patient patient = db.Patients.SingleOrDefault(p => p.ID == patientId);

            IQueryable<Examination> examinationsQuery = db.Examinations
                                                            .Include(exam => exam.Doctor)
                                                            .Include(exam => exam.Patient);
            PatientExaminationsListViewModel viewModel = new PatientExaminationsListViewModel();
            ExaminationTranslator patientExaminationTranslator = new ExaminationTranslator();
            if(!string.IsNullOrEmpty(searchByDoctorName))
            {
                examinationsQuery = examinationsQuery
                                    .Where(exam => exam.Doctor.FirstName.Contains(searchByDoctorName) || 
                                                exam.Doctor.LastName.Contains(searchByDoctorName));
            }

            examinationsQuery = examinationsQuery.Where(exam => exam.PatientID == patientId);
            var singlePatientExaminationQuery = examinationsQuery
                                                .Select(patientExaminationTranslator.ToPatientsExaminationsViewModel);

            switch (sort)
            {
                case "date_asc":
                    singlePatientExaminationQuery = singlePatientExaminationQuery.OrderBy(exam => exam.ExamDate);
                    break;
                default:
                    singlePatientExaminationQuery = singlePatientExaminationQuery.OrderByDescending(exam => exam.ExamDate);
                    break;
            }

            viewModel.Examinations = singlePatientExaminationQuery.ToPagedList(pageNumber, pageSize);
            viewModel.PatientFirstName = patient.FirstName;
            viewModel.PatientLastName = patient.LastName;
            viewModel.SortByDate = string.IsNullOrEmpty(sort) ? "date_asc" : "";
            viewModel.CurrentSort = sort;
            viewModel.DoctorNameFilter = searchByDoctorName;
            return View("ExaminationList", viewModel);
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
