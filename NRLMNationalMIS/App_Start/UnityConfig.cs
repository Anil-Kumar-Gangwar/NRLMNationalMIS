using NRLMNationalMIS.Services;
using NRLMNationalMIS.Services.IServices;
using NRLMNationalMIS.Data.Repositories.IRepositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using NRLMNationalMIS.Data.Repositories;

namespace NRLMNationalMIS.Web.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            #region "Login"
            container.RegisterType<ILoginService, LoginService>();
            #endregion
            #region "Generic"
            container.RegisterType<IGenericRepository, GenericRepository>();
            #endregion
            #region "Menu"
            container.RegisterType<IMenuService, MenuService>();
            #endregion
           
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}