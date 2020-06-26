using DoctorsOffice.Data;
using DoctorsOffice.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsOffice.Repositories
{
    public class DoctorsRepository : IRepository<Doctor>
    {
        private ApplicationDbContext _context;

        public DoctorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Doctor Create(Doctor obj)
        {
            Doctor newDoctor = _context.Doctors.Add(obj);
            return newDoctor;
        }

        public Doctor Delete(int id)
        {
            Doctor existing = _context.Doctors.SingleOrDefault(d => d.ID == id);
            var result = _context.Doctors.Remove(existing);
            return result;
        }

        public Doctor Edit(Doctor obj)
        {
            var updatedDoctor = _context.Doctors.Attach(obj);
            _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            return updatedDoctor;
        }

        public IQueryable<Doctor> GetAll()
        {
            return _context.Doctors;
        }

        public Doctor GetById(int id)
        {
            Doctor doctor = _context.Doctors.SingleOrDefault(d => d.ID == id);
            return doctor;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
