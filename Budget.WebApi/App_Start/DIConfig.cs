using Autofac;
using Autofac.Integration.WebApi;
using Budget.DAL;
using Budget.Models;
using Budget.Repository;
using Budget.Repository.Common;
using Budget.Service;
using Budget.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Budget.App_Start
{
    public static class DIConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // --- services ---
            builder.RegisterType<ExpenseService>().As<IExpenseService>();
            builder.RegisterType<IncomeService>().As<IIncomeService>();
            //builder.RegisterType<ExpenseRepository>().As<IExpenseRepository>(); //Ado.Net Repository
            // --- repositories ---
            builder.RegisterType<EfExpenseRepository>().As<IExpenseRepository>().InstancePerLifetimeScope(); 
            builder.RegisterType<EfIncomeRepository>().As<IIncomeRepository>().InstancePerLifetimeScope(); 
            // --- db context ---
            builder.RegisterType<BudgetV2Context>().InstancePerLifetimeScope();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}