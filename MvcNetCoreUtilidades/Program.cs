using MvcNetCoreUtilidades.Helpers;
using MvcNetCoreUtilidades.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Añadimos la memoria distribuida
builder.Services.AddDistributedMemoryCache();
//Esto es para recoger el nombre del server y el puerto para crear la url de los ficheros subidos
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<RepositoryCoches>();
builder.Services.AddScoped<HelperPathProvider>();
//Habilitamos session
builder.Services.AddSession();
builder.Services.AddMemoryCache();
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

app.UseAuthorization();

app.MapStaticAssets();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
