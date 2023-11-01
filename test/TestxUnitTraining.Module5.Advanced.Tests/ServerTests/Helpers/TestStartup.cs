using Chat.Server;
using Chat.Server.Library.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module5.Advanced.Tests.ServerTests.Helpers
{
    internal class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
            //Resto de registro de dependencias

            var connection  = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            services.AddDbContext<ChatDbContext>(opt => opt.UseSqlite(connection));

            var dbContext = services.BuildServiceProvider().GetRequiredService<ChatDbContext>();
            dbContext.Database.EnsureCreated();
            InitializationDatabase.InitializeContext(dbContext);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
