using Backend.BusinessLogic;
using Backend.BusinessLogic.Interface;
using Backend.Domain;
using Backend.Repository;
using Backend.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Backend.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<DbContext, BackendContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("TareaDB"))
            );
            services.AddScoped<ILogic<AdditionalFieldData>, AdditionalFieldDataLogic>();
            services.AddScoped<ILogic<AdditionalField>, AdditionalFieldLogic>();
            services.AddScoped<ILogic<Area>, AreaLogic>();
            services.AddScoped<IRequestLogic, RequestLogic>();
            services.AddScoped<IUserSession, SessionLogic>();
            services.AddScoped<ILogic<Topic>, TopicLogic>();
            services.AddScoped<ITopicTypeLogic, TopicTypeLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IDataImportLogic, DataImportLogic>();

            services.AddScoped<IRepository<AdditionalFieldData>, AdditionalFieldDataRepository>();
            services.AddScoped<IRepository<AdditionalField>, AdditionalFieldRepository>();
            services.AddScoped<IRepository<Area>, AreaRepository>();
            services.AddScoped<IRepository<Request>, RequestRepository>();
            services.AddScoped<IRepository<UserSession>, SessionRepository>();
            services.AddScoped<IRepository<Topic>, TopicRepository>();
            services.AddScoped<IRepository<TopicType>, TopicTypeRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IMMRequest API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
