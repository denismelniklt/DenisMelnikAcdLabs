using AutoMapper;
using AutoMapper.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

using DataLineManagerBll = BLL.DataLineManager;
using DataLineManagerDal = DAL.DataLineManager;
using IDataLineManagerBll = BLL.IDataLineManager;
using IDataLineManagerDal = DAL.IDataLineManager;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            InitializeAutomapper();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDataLineManagerDal, DataLineManagerDal>();
            services.AddTransient<IDataLineManagerBll, DataLineManagerBll>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ApplicationEnvironment.InputFilePath = Configuration["FilePath"];

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }

        private void InitializeAutomapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");
                cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "ViewModel");
            });
        }
    }
}