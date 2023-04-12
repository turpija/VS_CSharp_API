using Autofac;
using Budget.Service.Common;
using Budget.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Controllers
{
    public class ControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExpenseService>().As<IExpenseService>();
        }
    }
}