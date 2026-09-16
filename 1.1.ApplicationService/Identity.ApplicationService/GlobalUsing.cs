global using AutoMapper;
global using Microsoft.Extensions.Logging;
global using FluentValidation;
global using FluentValidation.Results;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.Http;


global using Identity.ApplicationService.Utilities;
global using Identity.ApplicationService.Common;
global using Identity.Domain.Common;
global using Identity.Repository.Common;
global using Identity.ApplicationService.Contracts;
global using System.Net;
global using Identity.Domain.IDentityContent;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;


global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.Extensions.Options;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;


// category namespace
global using Identity.ApplicationService.Categories.Entites;
global using Identity.ApplicationService.Categories.Response;
global using Identity.Domain.Categories;
global using Identity.Repository.Categories;


// courses namespaces
global using Identity.ApplicationService.Courses.Entities;
global using Identity.ApplicationService.Courses.Response;
global using Identity.Domain.Courses;
global using Identity.Repository.Courses;



// teachers namespaces
global using Identity.Domain.Teachers;
global using Identity.ApplicationService.Teachers.Entities;
global using Identity.ApplicationService.Teachers.Response;
global using Identity.Repository.Teachers; 


// users namespaces
global using Identity.ApplicationService.Users.Entites;
global using Identity.ApplicationService.Users.Response;
global using Identity.ApplicationService.Users.Validators;
global using Identity.ApplicationService.Users.Services;
global using Identity.Domain.Users;
global using Identity.Repository.Users;

// role namesapces
global using Identity.ApplicationService.Roles.Entities;
global using Identity.ApplicationService.Roles.Response;
global using Identity.ApplicationService.Roles.Validators;
global using Identity.ApplicationService.Roles.Services;


// account namesoaces
global using Identity.ApplicationService.Account.Entites;
global using Identity.ApplicationService.Account.Response;
global using Identity.ApplicationService.Account.Validators;


// token namespaces
global using Identity.ApplicationService.Tokens.Entities;
global using Microsoft.IdentityModel.Tokens;
