using Microsoft.EntityFrameworkCore;
using RareNFTs.Application.Profiles;
using RareNFTs.Application.Services.Implementations;
using RareNFTs.Application.Services.Interfaces;
using RareNFTs.Infraestructure.Data;
using RareNFTs.Infraestructure.Repository.Implementation;
using RareNFTs.Infraestructure.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Configure D.I.
builder.Services.AddTransient<IRepositoryCard, RepositoryCard>();
builder.Services.AddTransient<IRepositoryClient, RepositoryClient>();
builder.Services.AddTransient<IRepositoryCountry, RepositoryCountry>();
builder.Services.AddTransient<IRepositoryNft, RepositoryNft>();
builder.Services.AddTransient<IRepositoryInvoice, RepositoryInvoice>();
//Services
builder.Services.AddTransient<IServiceCountry, ServiceCountry>();
builder.Services.AddTransient<IServiceClient, ServiceClient>();
builder.Services.AddTransient<IServiceCard, ServiceCard>();
builder.Services.AddTransient<IServiceNft, ServiceNft>();
builder.Services.AddTransient<IServiceInvoice, ServiceInvoice>();
builder.Services.AddTransient<IServiceReport, ServiceReport>();


// config Automapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CardProfile>();
    config.AddProfile<CountryProfile>();
    config.AddProfile<ClientProfile>();
    config.AddProfile<NftProfile>();
    config.AddProfile<InvoiceProfile>();
    config.AddProfile<ClientNftProfile>();
});

// Config Connection to SQLServer DataBase
builder.Services.AddDbContext<RareNFTsContext>(options =>
{
    // it read appsettings.json file
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDataBase"));

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Configure Swagger 
builder.Services.AddSwaggerGen();




var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Error control Middleware
    //app.UseMiddleware<ErrorHandlingMiddleware>();
}
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  // Esto asegura que se use el enrutamiento por atributos
});

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
