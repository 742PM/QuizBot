using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using QuizWebHookBot.Services;

namespace QuizWebHookBot
{
    public class Startup
    {
        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IKernel Kernel { get; set; }

        public IConfiguration Configuration { get; }

        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            // IKernelConfiguration config = new KernelConfiguration();
            var kernel = new StandardKernel();

            // Register application services
            foreach (var ctrlType in app.GetControllerTypes()) kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);

            // This is where our bindings are configurated
            //kernel.Bind<ITestService>().To<TestService>().InScope(RequestScope);

            // Cross-wire required framework services
            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

            return kernel;
        }

        private object Resolve(Type type) => Kernel.Get(type);
        private object RequestScope(IContext context) => scopeProvider.Value;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddScoped<ProcessUpdate,>();
            services.AddScoped<IUpdateService, UpdateService>();
            services.AddSingleton<IBotService, BotService>();

            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Kernel = RegisterApplicationComponents(app);
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private sealed class Scope : DisposableObject
        {
        }

        public sealed class DelegatingViewComponentActivator : IViewComponentActivator
        {
            private readonly Func<Type, object> viewComponentCreator;
            private readonly Action<object> viewComponentReleaser;

            public DelegatingViewComponentActivator(
                Func<Type, object> viewComponentCreator,
                Action<object> viewComponentReleaser = null)
            {
                this.viewComponentCreator = viewComponentCreator ??
                                            throw new ArgumentNullException(nameof(viewComponentCreator));
                this.viewComponentReleaser = viewComponentReleaser ?? (_ => { });
            }

            public object Create(ViewComponentContext context) =>
                viewComponentCreator(context.ViewComponentDescriptor.TypeInfo.AsType());

            public void Release(ViewComponentContext context, object viewComponent) =>
                viewComponentReleaser(viewComponent);
        }
    }
}
