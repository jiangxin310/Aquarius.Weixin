﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Aquarius.Weixin.Core.Configuration.DependencyInjection;
using Aquarius.Weixin.Core.Message.Handler;
using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Web.MessageReply;

namespace Aquarius.Weixin.Web
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
            services.AddMvc();

            services.AddAquariusWeixin(opt =>
            {
                opt.BaseSetting = new BaseSettings()
                {
                    Debug = true,
                    IsRepetValid = false,
                    AppId = "your appid",
                    AppSecret = "your appsecret",
                    Token = "token"
                };
                opt.CacheType = CacheType.InMemory;
                opt.MsgMiddlewareType = MessageMiddlewareType.Plain;
            });

            services.AddScoped<ClickEvtMessageHandlerBase, ClickEventReplyTextHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
