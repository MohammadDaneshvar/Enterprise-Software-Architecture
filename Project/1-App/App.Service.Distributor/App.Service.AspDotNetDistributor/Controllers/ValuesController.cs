using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Framework.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using AppService.Common;
using Framework.Application.Common.Extentions;
using Framework.Data.EF;
using System.Reflection;

namespace DynamicAndGenericControllersSample.Controllers
{
    [Route("api/[controller]")]
    public class BaseController<T> : Controller where T : IRestrictedCommand
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandBus _commandBus;

        public BaseController(IServiceProvider serviceProvider)
        {
            this._serviceProvider = Startup._container;
            this._commandBus = _serviceProvider.GetService<ICommandBus>();
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(T command)
        {
            var result = await MakeHandlerAsync(command);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(T command)
        {
            var result = await MakeHandlerAsync(command);
            return result;
        }

        public async Task<IActionResult> MakeHandlerAsync(T command)
        {
            IActionResult result = null;
            try
            {
                using (AsyncScopedLifestyle.BeginScope(Startup._container))
                    await _commandBus.DispatchAsync(command);
                if (command.HaveResult())
                {
                    var commandResult = command.GetResult();
                    result = Ok(commandResult);
                }
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
