using DoctorsOffice.Data;
using DoctorsOffice.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsOffice.Repositories
{
    public class PatientsRepository : IRepository<Patient>
    {
        private ApplicationDbContext _context;

        public PatientsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Patient Create(Patient obj)
        {
            Patient newPatient = _context.Patients.Add(obj);
            return newPatient;
        }

        public Patient Delete(int id)
        {
            Patient existing = _context.Patients.SingleOrDefault(p => p.ID == id);
            var result = _context.Patients.Remove(existing);
            return result;
        }

        public Patient Edit(Patient obj)
        {
            var updatedPatient = _context.Patients.Attach(obj);
            _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            return updatedPatient;
        }

        public IQueryable<Patient> GetAll()
        {
            return _context.Patients;
        }

        public Patient GetById(int id)
        {
            Patient patient = _context.Patients.SingleOrDefault(p => p.ID == id);
            return patient;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
