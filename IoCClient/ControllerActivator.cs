using CustomIoCContainer;
using IoCClient.Controllers;
using IoCClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoCClient
{
    public class ControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext context)
        {
            return Activator.CreateInstance(context.ActionDescriptor.ControllerTypeInfo.AsType(), new object[] { IoCContainer.Resolve<MyService>() });
        }

        public void Release(ControllerContext context, object controller)
        {
            if (controller is Controller disposableController)
            {
                disposableController.Dispose();
            }
        }
    }
}
