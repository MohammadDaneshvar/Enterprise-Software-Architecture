//using AppService.Config;
//using Framework.Application;
//using Framework.Application.Config;
//using NUnit.Framework;
//using SimpleInjector;
//using SimpleInjector.Lifestyles;
//using System.Configuration;

//namespace AppService.Test
//{
//    public class Tests
//    {
//        private static Container container;
//        private string ConnectionString
//        {
//            get
//            {
//                return ConfigurationManager.ConnectionStrings["Core"].ConnectionString;
//            }
//        }
//        [SetUp]
//        public void Setup()
//        {
//            container = new Container();
//            container.Options.DefaultScopedLifestyle =
//                Lifestyle.CreateHybrid(new AsyncScopedLifestyle(), new ThreadScopedLifestyle());
//            FrameworkConfigurator.WireUp(container, true, typeof(AppService).Assembly);
//            Config.SaleConfigurator.WireUp(container);
//            container.Register<IEventHandlerFactory>(() => new EventHandlerFactory(container), Lifestyle.Singleton);
//        }

//        [Test]
//        public void Test1()
//        {
//            var cmd = new CreateOrderCommand
//            {
//                CustomerId = 1,
//                OrderLines = new List<OrderLineDto>
//                {
//                    new OrderLineDto() {ProductId = 10, Quantity = 20}
//                }
//            };
//            var bus = container.GetInstance<ICommandBus>();
//            using (AsyncScopedLifestyle.BeginScope(container))
//            {
//                bus.Dispatch(cmd);
//            }
//        }
//        [TestMethod]
//        public void TestMethodHaveResult()
//        {
//            var cmd = new GetOrderByIdQuery()
//            {
//                Id = 7
//            };
//            var bus = container.GetInstance<ICommandBus>();
//            using (AsyncScopedLifestyle.BeginScope(container))
//            {
//                bus.Dispatch(cmd);
//                bus.Dispatch(cmd);
//                Console.WriteLine(cmd.As<OrderDto>().CustomerId);

//            }
//        }
//    }
//}