using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Framework.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace DynamicAndGenericControllersSample.Controllers
{
    [Route("api/[controller]")]
    public class BaseController<T> : Controller where T : class
    {
        private Storage<T> _storage;
        private readonly IServiceProvider _serviceProvider;

        public BaseController(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(T command)
        {
            var result= await MakeHandler(command);
            return  result;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(T command)
        {
            var result = await MakeHandler(command);
            return result;
        }

        public async Task<IActionResult> MakeHandler(T command )
        {
            IActionResult result =null;
            try
            {
                var s = _serviceProvider.GetService(typeof( IServiceProvider));
                var bus =(ICommandBus) _serviceProvider.GetService(typeof(ICommandBus));
                using (AsyncScopedLifestyle.BeginScope((Container) _serviceProvider))
                    await bus.DispatchAsync(command);
                if (command is IHaveResult)
                    result = Ok(((IHaveResult)command).Result);
                else
                    result = Ok();
            }
            catch (Exception e)
            {
                result = BadRequest(e.Message);
            }
            return result;
           
        }
    }
}
