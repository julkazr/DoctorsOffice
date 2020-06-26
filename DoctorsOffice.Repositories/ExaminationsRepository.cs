using DoctorsOffice.Data;
using DoctorsOffice.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsOffice.Repositories
{
    public class ExaminationsRepository : IRepository<Examination>
    {
        private ApplicationDbContext _context;

        public ExaminationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Examination Create(Examination obj)
        {
            Examination newExamination = _context.Examinations.Add(obj);
            return newExamination;
        }

        public Examination Delete(int id)
        {
            Examination existing = _context.Examinations.SingleOrDefault(exam => exam.ID == id);
            var result = _context.Examinations.Remove(existing);
            return result;
        }

        public Examination Edit(Examination obj)
        {
            var updatedExamination = _context.Examinations.Attach(obj);
            _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            return updatedExamination;
        }

        public IQueryable<Examination> GetAll()
        {
            return _context.Examinations;
        }

        public Examination GetById(int id)
        {
            Examination patient = _context.Examinations.SingleOrDefault(exam => exam.ID == id);
            return patient;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
