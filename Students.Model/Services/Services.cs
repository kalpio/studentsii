using System;
using Microsoft.Extensions.DependencyInjection;

namespace Students.Model.Services
{
    public static class Services
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection ServiceCollection { get; set; } = new ServiceCollection();


        public static void ConfigureServices()
        {
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }
    }
}
