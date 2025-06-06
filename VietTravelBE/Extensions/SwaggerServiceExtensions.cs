﻿using Microsoft.OpenApi.Models;
using VietTravelBE.Dtos;

namespace VietTravelBE.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TRAVEL API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
                c.AddSecurityRequirement(securityRequirement);

                c.MapType<List<TourStartDateDto>>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "json"
                });

                c.MapType<List<TourScheduleDto>>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "json"
                });

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumention(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c
                .SwaggerEndpoint("/swagger/v1/swagger.json", "SkiNet API v1");
            });

            return app;
        }
    }
}
