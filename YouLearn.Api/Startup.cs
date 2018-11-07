using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using YouLearn.Api.Security;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Domain.Services;
using YouLearn.Infra.Persistence.EF;
using YouLearn.Infra.Persistence.Repositories;
using YouLearn.Infra.Transactions;

namespace YouLearn.Api
{
    public class Startup
    {
        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6bbbb645024";

        public void ConfigureServices(IServiceCollection services)
        {
            //Adicionando injeção de Independência
            services.AddScoped<YouLearnContext, YouLearnContext>();
            //Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServicePlayList, ServicePlayList>();
            services.AddScoped<IServiceCanal, ServiceCanal>();
            services.AddScoped<IServiceVideo, ServiceVideo>();
            services.AddScoped<IServiceUsuario, ServiceUsuario>();
            //Repositories
            services.AddScoped<IRepositoryCanal, RepositoryCanal>();
            services.AddScoped<IRepositoryPlayList, RepositoryPlayList>();
            services.AddScoped<IRepositoryVideo, RepositoryVideo>();
            services.AddScoped<IRepositoryUsuario, RepositoryUsuario>();

            //Configuração do Token
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations
            {
                Audience = AUDIENCE,
                Issuer = ISSUER,
                Seconds = int.Parse(TimeSpan.FromDays(1).TotalSeconds.ToString())
            };
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions => {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => {

                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.SigningCredentials.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                //Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                //Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                //Tempo de tolerância para expiração de um token
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar acesso
            // a recursos deste projeto
            services.AddAuthorization(auth => {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            // Para todas as requisições serem necessárias o token, para um enpoint não exigier o token
            // deve colocar o [AllowAnonymous]
            // Caso remova essa linha, para todas as requisições que precisar de token, deve colocar 
            // o atributo [Authorize("Bearer")]
            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder()
                             .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                             .RequireAuthenticatedUser().Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //CORS
            services.AddCors();

            //Aplicando documentação com swagger
            services.AddSwaggerGen( x => {
                x.SwaggerDoc("v1", new Info { Title="YouLearn", Version="v1"});
            });

            //services.AddDistributedMemoryCache();
            //services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //CORS
            app.UseCors(x=> {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","YouLearn");
            });

            //app.UseSession();

            //app.run(async (context) =>
            //{
            //    await context.response.writeasync("hello world!");
            //});
        }
    }
}
