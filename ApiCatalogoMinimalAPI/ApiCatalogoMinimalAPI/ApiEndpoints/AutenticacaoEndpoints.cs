﻿using ApiCatalogoMinimalAPI.Models;
using ApiCatalogoMinimalAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiCatalogoMinimalAPI.ApiEndpoints;
public static class AutenticacaoEndpoints
{
    public static void MapAutenticacaoEndpoints(this WebApplication app)
    {
        //Endpoint para login
        app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
        {
            if (userModel == null)
                return Results.BadRequest("Login inválido");

            if (userModel.Name == "Wilson" && userModel.Password == "NumSey#123")
            {
                var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"],
                    app.Configuration["Jwt:Issuer"],
                    app.Configuration["Jwt:Audience"],
                    userModel);

                return Results.Ok(new { token = tokenString });
            }
            else
            {
                return Results.BadRequest("Login inválido");
            }
        }).Produces(StatusCodes.Status400BadRequest)
                        .Produces(StatusCodes.Status200OK)
                        .WithName("Login")
                        .WithTags("Autenticacao");
    }
}
