using IwwageNationalMIS.Services;
using IwwageNationalMIS.Services.IServices;
using IwwageNationalMIS.Data.Repositories.IRepositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using IwwageNationalMIS.Data.Repositories;

namespace IwwageNationalMIS.Web.App_Start
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