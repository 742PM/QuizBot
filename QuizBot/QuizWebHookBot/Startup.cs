﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using QuizBotCore;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using QuizWebHookBot.Services;

namespace QuizWebHookBot
{
    public class Startup
    {
        private const string DatabaseName = "telegramUsers";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IMongoDatabase CreateDatabase(string connectionString) =>
            new MongoClient(connectionString).GetDatabase(DatabaseName);
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IUpdateService, UpdateService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddScoped<IQuizService, Requester>();
            services.AddSingleton<IMongoDatabase>(CreateDatabase(""));

            services.AddSingleton<IUserRepository, MongoUserRepository>();
            services.AddScoped<IStateMachine<ICommand>, TelegramStateMachine>();
            services.AddScoped<IMessageParser, MessageParser>();

            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
