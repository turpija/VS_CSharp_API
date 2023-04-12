using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Budget.Controllers;
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

            // --- modules ---
            builder.RegisterModule(new ControllerModule());

            // --- services ---
            //builder.RegisterType<ExpenseService>().As<IExpenseService>();
            builder.RegisterType<IncomeService>().As<IIncomeService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();

            // --- repositories ---
            builder.RegisterType<ExpenseRepository>().As<IExpenseRepository>().InstancePerLifetimeScope();
            builder.RegisterType<IncomeRepository>().As<IIncomeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();

            // --- db context ---
            builder.RegisterType<BudgetV2Context>().InstancePerLifetimeScope();


            // --- auto mapper ---
            builder.Register(context => new MapperConfiguration(cfg =>
                 {
                     cfg.AddProfile<AutoMapperProfile>();
                 })).AsSelf().SingleInstance();

            builder.Register(context => context.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}