using DoctorsOffice.Domain.Interfaces;
using DoctorsOffice.Domain.Models;
using DoctorsOffice.Repositories;
using DoctorsOffice.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace DoctorsOffice.Domain.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorsRepository _doctorsRepository;
        public DoctorService(IDoctorsRepository doctorsRepository)
        {
            _doctorsRepository = doctorsRepository;
        }

        public List<DoctorDomainModel> GetAll(string sort, string searchByName, string searchByPosition, int? page)
        {
            //int pageSize = 3;
            //int pageNumber = (page ?? 1);
            var doctorTranslator = new DoctorTranslator();
            var data = _doctorsRepository.GetAll();
            if ( data == null )
            {
                return null;
            }
            if (!string.IsNullOrEmpty(searchByName))
            {
                data = data
                    .Where(d => d.FirstName.Contains(searchByName) || d.LastName.Contains(searchByName));

            }
            if (!string.IsNullOrEmpty(searchByPosition))
            {
                data = data.Where(d => d.Position.Contains(searchByPosition));

            }

            switch (sort)
            {
                case "name_desc":
                    data = data.OrderByDescending(d => d.LastName).ThenByDescending(d => d.FirstName);
                    break;
                case "position":
                    data = data.OrderBy(d => d.Position);
                    break;
                case "position_desc":
                    data = data.OrderByDescending(d => d.Position);
                    break;
                default:
                    data = data.OrderBy(d => d.LastName).ThenBy(d => d.FirstName);
                    break;
            }
            var translatedData = data.Select(doctorTranslator.ToDomainModel)
                    .ToList()
                    ;
            List<DoctorDomainModel> result = new List<DoctorDomainModel>();
            DoctorDomainModel model;
            foreach(var item in translatedData)
            {
                model = new DoctorDomainModel()
                {
                    ID = item.ID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    Position = item.Position,
                    Address = item.Address
                };
                result.Add(model);
            }
            return result;

        }

        public Task<DoctorDomainModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<DoctorDomainModel> Create(DoctorDomainModel newDoctor)
        {
            throw new NotImplementedException();
        }

        public Task<DoctorDomainModel> Delete(DoctorDomainModel doctorToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<DoctorDomainModel> Edit(DoctorDomainModel doctor)
        {
            throw new NotImplementedException();
        }

    }
}
