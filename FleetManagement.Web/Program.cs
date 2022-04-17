using FleetManagement.Data.Repository;
using FleetManagement.Data.Services;
using FleetManagement.Web.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add services to the IOC container.
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCookieAuthentication();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else {
    using (var scope = app.Services.CreateScope()) 
    {
        var serviceProvider = scope.ServiceProvider;

        var context = serviceProvider.GetRequiredService<DataContext>();
        context.Initialise();
        Seeder.Seed(new VehicleService(context), new UserService(context));
    }
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



// todo 
// image for vehicles
// dashboard
// testing
