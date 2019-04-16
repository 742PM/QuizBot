﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;

namespace QuizWebHookBot
{
    public static class AspNetCoreExtensions
    {
        public static void AddCustomViewComponentActivation(
            this IServiceCollection services,
            Func<Type, object> activator)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (activator == null) throw new ArgumentNullException(nameof(activator));

            services.AddSingleton<IViewComponentActivator>(new Startup.DelegatingViewComponentActivator(activator));
        }

        public static void AddRequestScopingMiddleware(
            this IServiceCollection services,
            Func<IDisposable> requestScopeProvider)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (requestScopeProvider == null) throw new ArgumentNullException(nameof(requestScopeProvider));

            services.AddSingleton<IStartupFilter>(new RequestScopingStartupFilter(requestScopeProvider));
        }

        public static void AddCustomControllerActivation(this IServiceCollection services, Func<Type, object> activator)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (activator == null) throw new ArgumentNullException(nameof(activator));

            services.AddSingleton<IControllerActivator>(new DelegatingControllerActivator(context =>
                                                                                              activator(context
                                                                                                        .ActionDescriptor
                                                                                                        .ControllerTypeInfo
                                                                                                        .AsType())));
        }
    }
}