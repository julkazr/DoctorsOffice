using DoctorsOffice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsOffice.Domain.Interfaces
{
    public interface IDoctorService
    {
        List<DoctorDomainModel> GetAll(string sort, string searchByName, string searchByPosition, int? page);
        Task<DoctorDomainModel> GetByIdAsync(int id);
        Task<DoctorDomainModel> Create(DoctorDomainModel newDoctor);
        Task<DoctorDomainModel> Edit(DoctorDomainModel doctor);
        Task<DoctorDomainModel> Delete(DoctorDomainModel doctorToDelete);
    }
}
