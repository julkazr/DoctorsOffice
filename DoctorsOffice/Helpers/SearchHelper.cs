using DoctorsOffice.Data;
using DoctorsOffice.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DoctorsOffice.Helpers
{
    public class SearchHelper
    {
        public IQueryable<T> SearchByName<T>(IQueryable<T> personsQuery, string searchByName)
        {
            //if (!string.IsNullOrEmpty(searchByName))
            //{
            //    personsQuery = personsQuery
            //        .Where(d => d.FirstName.Contains(searchByName) || d.LastName.Contains(searchByName));

            //}
            
            return personsQuery;
        }
    }       
   
}