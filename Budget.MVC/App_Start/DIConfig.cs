using Autofac;
using Autofac.Integration.Mvc;
using Budget.DAL;
using Budget.Repository;
using Budget.Repository.Common;
using Budget.Service;
using Budget.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Configuration;

namespace Budget.MVC.App_Start
{
    public static class DIConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // --- services ---
            builder.RegisterType<ExpenseService>().As<IExpenseService>();
            builder.RegisterType<IncomeService>().As<IIncomeService>();
            // --- repositories ---
            builder.RegisterType<EfExpenseRepository>().As<IExpenseRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfIncomeRepository>().As<IIncomeRepository>().InstancePerLifetimeScope();
            // --- db context ---
            builder.RegisterType<BudgetV2Context>().InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}