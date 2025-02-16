using FUNewsManagement.Repositories.Extensions;
using FUNewsManagement.services.Extensions;
using FUNewsManagementMVC.Authentications;
using FUNewsManagementMVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// =========================
// === Add Services
// =========================

builder.Services.AddControllersWithViews();
builder.Services.AddRepositoriesLayer(builder.Configuration);
builder.Services.AddServicesLayer();
builder.Services.AddApplicationLayer();

// Add singleton adminAccount
var adminAccount = new AdminCredentials()
{
    AccountEmail = builder.Configuration["AdminCredentials:email"],
    AccountPassword = builder.Configuration["AdminCredentials:password"],
};

builder.Services.AddSingleton<AdminCredentials>(adminAccount);

// =========================
// === Build App
// =========================

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
name: "default",
pattern: "{controller=SystemAccounts}/{action=Login}/{id?}");

app.Run();
