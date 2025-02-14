using AutoMapper;
using FUNewsManagement.Repositories.Extensions;
using FUNewsManagement.services.Extensions;
using FUNewsManagementMVC;

var builder = WebApplication.CreateBuilder(args);

// AddAsync all services.
builder.Services.AddControllersWithViews();
builder.Services.AddRepositoriesLayer(builder.Configuration);
builder.Services.AddServicesLayer();

// AddAsync automapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); //Set session timeout
    options.Cookie.HttpOnly = true; // For sercurity
    options.Cookie.IsEssential = true; // Ensure session cookie is always created
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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
name: "default",
pattern: "{controller=SystemAccounts}/{action=Login}/{id?}");

app.Run();
