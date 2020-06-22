using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using AppService;
using DynamicAndGenericControllersSample.Controllers;
using Framework.Application;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace DynamicAndGenericControllersSample
{
    public class RemoteControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly IServiceProvider _serviceProvider;

        public RemoteControllerFeatureProvider(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            //var remoteCode = new HttpClient().GetStringAsync("https://gist.githubusercontent.com/filipw/9311ce866edafde74cf539cbd01235c9/raw/6a500659a1c5d23d9cfce95d6b09da28e06c62da/types.txt").GetAwaiter().GetResult();
            //if (remoteCode != null)
            //{
            //    var compilation = CSharpCompilation.Create("DynamicAssembly", new[] { CSharpSyntaxTree.ParseText(remoteCode) },
            //        new[] {
            //        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            //        MetadataReference.CreateFromFile(typeof(RemoteControllerFeatureProvider).Assembly.Location)
            //        },
            //        new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            //    using (var ms = new MemoryStream())
            //    {
            //        var emitResult = compilation.Emit(ms);

            //        if (!emitResult.Success)
            //        {
            //            // handle, log errors etc
            //            Debug.WriteLine("Compilation failed!");
            //            return;
            //        }

            //        ms.Seek(0, SeekOrigin.Begin);
            //        var assembly = Assembly.Load(ms.ToArray());
            //        var candidates = assembly.GetExportedTypes();


            var commandHandlers = ((Container)_serviceProvider).GetTypesToRegister(typeof(ICommandHandler<>), new[] { typeof(LoanAppService).Assembly });
            var commands = commandHandlers.SelectMany(type => type.GetInterfaces())
                            .Where(type => type.IsGenericType)
                            .Select(type => type.GetGenericArguments().First());

            foreach (var command in commands)
            {
                using (AsyncScopedLifestyle.BeginScope(Startup._container))
                    feature.Controllers.Add(typeof(BaseController<>).MakeGenericType(command).GetTypeInfo());
            }
            //  }
            //   }
        }
    }
}
