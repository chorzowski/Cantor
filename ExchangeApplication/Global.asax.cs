using Autofac;
using Autofac.Integration.Mvc;
using ExchangeApplication.Models;
using ExchangeApplication.Utilities;
using ExchangeApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ExchangeApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureContainer();
          

            //    var builder = new ContainerBuilder();


            //    builder.RegisterType<SaveData>().As<ISaveData>();

            ////    builder.RegisterControllers(typeof(MvcApplication).Assembly);


            //    builder.Build();

            //    // ContainerConfig();
        }

        private void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterType<SaveData>().As<ISaveData>();
            builder.RegisterType<GetJeson>().As<IGetJeson>();
            builder.RegisterType<GetUserId>().As<IGetUserId>();
            builder.RegisterType<BuyMoney>().As<IBuyMoney>();
            builder.RegisterType<SellMoney>().As<ISellMoney>();
            builder.RegisterType<JsonIdUser>().As<IJsonIdUser>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
