using DoctorsOffice.Controllers;
using DoctorsOffice.DbContexts;
using DoctorsOffice.Domain.Interfaces;
using DoctorsOffice.Domain.Services;
using DoctorsOffice.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace DoctorsOffice
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<IDoctorsRepository, DoctorsRepository>();
            container.RegisterType<IDoctorService, DoctorService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}