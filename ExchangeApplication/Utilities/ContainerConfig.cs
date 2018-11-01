using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeApplication.Utilities
{
    public static class ContainerConfig
    {
        //public static IContainer Configure()
        //{
        //    var builder = new ContainerBuilder();

        //    builder.RegisterType<GetJeson>().As<IGetJeson>();

        // To tu nie działa, wrzucone wszystko w Global.asax

        //    builder.RegisterType<GetJeson>().As<IGetJeson>().SingleInstance().PreserveExistingDefaults();

        //    builder.RegisterType<SaveData>().As<ISaveData>();

        //    builder.RegisterControllers(typeof(MvcApplication).Assembly);

        //    return builder.Build();
        //}
    }
}