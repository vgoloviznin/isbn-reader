using Data;
using LightInject;
using Services.Books;

namespace IsbnReader
{
    public static class IocConfig
    {
        public static void Register()
        {
            var container = new ServiceContainer();
            container.RegisterControllers();

            container.Register<Context, Context>(new PerScopeLifetime());
            container.Register<IBookService, BookService>(new PerScopeLifetime());
            container.Register<IIsbnService, IsbnService>(new PerScopeLifetime());

//            container.EnablePerWebRequestScope();
            container.EnableMvc();
        }
    }
}