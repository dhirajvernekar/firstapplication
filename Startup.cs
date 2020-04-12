using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DatingApp.api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using DatingApp.api.Helper;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;
using AutoMapper;

namespace DatingApp.api
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
            services.AddDbContext<DataContext>(x=>x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().AddNewtonsoftJson(opt =>{
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
           
            //services.AddControllers().AddNewtonsoftJson();
    //         services.AddControllers().AddNewtonsoftJson(options =>
    // {
    //     options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    // });
        // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
        //    .AddJsonOptions(opt=> {
        //         opt.JsonSerializerOptions.ref=
        //         Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        //     });
            //services.AddMvc();
            services.AddCors();
           // services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(DatingRepository).Assembly);
            services.AddScoped<IAuthRepository,AuthRepository>();
            services.AddScoped<IDatingRepository,DatingRepository>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>{
                    builder.Run(async context => {
                        context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

                        var error=context.Features.Get<IExceptionHandlerFeature>();
                        if (error!=null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                           await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //app.UseCors(x => x.WithOrigins("http://localhost:4200"));
              // .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            
        }
    }
}
