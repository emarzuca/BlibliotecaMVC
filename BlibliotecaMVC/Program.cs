using BlibliotecaMVC;
using BlibliotecaMVC.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();




// Add services to the container.

//establecemos ls politicas de autentificacion arriba mencionadas
builder.Services.AddControllersWithViews(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
});

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRepositorioApartados, RepositorioApartados>();
builder.Services.AddTransient<IRepositorioSubApartados, RepositorioSubApartados>();
builder.Services.AddTransient<IRepositorioDash, RepositorioDash>();
builder.Services.AddDbContext<AplicationDbContext>(opciones =>
opciones.UseSqlServer("name=DefaultConnection"));

//**Generamos los servicios de autentificación para que el usuario se pueda loguear
builder.Services.AddAuthentication();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<AplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddAutoMapper(typeof(Program));

//** Configuracion para No utilizart las vistas por defecto que da Identity a través de nuestro esquema de autentificación

builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones=>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath= "/usuarios/login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



// ** Obtenemos la Data del Usuario Autentificado a través de :
app.UseAuthentication();
// **

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
