using Autofac;
using Autofac.Integration.Mvc;
using ExchangeApplication.Utilities;
using ExchangeApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExchangeApplication.App_Start
{
    public class DependencyRegistrator
    {
        public static void ConfigureContainer()
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