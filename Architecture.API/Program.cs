using System.Reflection;
using Architecture.API.Filters;
using Architecture.API.Middlewares;
using Architecture.API.Modules;
using Architecture.Caching;
using Architecture.Core.Abstract.Repositories;
using Architecture.Core.Abstract.Services;
using Architecture.Core.Abstract.UnitOfWork;
using Architecture.Repository;
using Architecture.Repository.Concrete.Repositories;
using Architecture.Repository.Concrete.UnitOfWork;
using Architecture.Service.Abstract.Services;
using Architecture.Service.Mapper;
using Architecture.Service.Validators;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new ValidateFilterAttribute());
    })
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(opts =>
{
    opts.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlServer"), opts =>
    {
        opts.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductService, ProductServiceWithCaching>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();



builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MapProfile)));
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddMemoryCache();

// auto fac
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(cb => cb.RegisterModule(new RepoServiceModule()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
