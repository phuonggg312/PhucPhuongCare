using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.AdminPortal.Data;
using PhucPhuongCare.Data;
using PhucPhuongCare.DataStore.EFCore.Repositories;
using PhucPhuongCare.DataStore.EFCore;
using PhucPhuongCare.UseCases.AppointmentsUseCases;
using PhucPhuongCare.UseCases.DoctorsUseCases;
using PhucPhuongCare.UseCases.PluginInterfaces;
using PhucPhuongCare.UseCases.SpecialtiesUseCases;
using PhucPhuongCare.UseCases.TimeSlotsUseCases;
using PhucPhuongCare.UseCases.SchedulesUseCases;


var builder = WebApplication.CreateBuilder(args);
// ===== B?T ??U KH?I CODE C?N COPY =====

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// C?u hình AppDbContext VÀ ApplicationDbContext, ch? ??nh rõ Migrations Assembly
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("PhucPhuongCare.DataStore.EFCore")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("PhucPhuongCare.DataStore.EFCore")));

// C?u hình Identity (?? Admin c?ng có th? ??ng nh?p)
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // << THÊM DÒNG NÀY
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ??ng ký Repositories
builder.Services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IDoctorScheduleRepository, DoctorScheduleRepository>();
builder.Services.AddTransient<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();

// ??ng ký Use Cases
builder.Services.AddTransient<IViewSpecialtiesUseCase, ViewSpecialtiesUseCase>();
builder.Services.AddTransient<IViewDoctorsBySpecialtyUseCase, ViewDoctorsBySpecialtyUseCase>();
builder.Services.AddTransient<IViewDoctorByIdUseCase, ViewDoctorByIdUseCase>();
builder.Services.AddTransient<IViewAvailableTimeSlotsUseCase, ViewAvailableTimeSlotsUseCase>();
builder.Services.AddTransient<IGenerateTimeSlotsUseCase, GenerateTimeSlotsUseCase>();
builder.Services.AddTransient<IBookAppointmentUseCase, BookAppointmentUseCase>();
builder.Services.AddTransient<IViewMyAppointmentsUseCase, ViewMyAppointmentsUseCase>();
builder.Services.AddTransient<IAdminViewAllDoctorsUseCase, AdminViewAllDoctorsUseCase>();
builder.Services.AddTransient<IAdminAddDoctorUseCase, AdminAddDoctorUseCase>();
builder.Services.AddTransient<IAdminUpdateDoctorUseCase, AdminUpdateDoctorUseCase>();
builder.Services.AddTransient<IAdminDeleteDoctorUseCase, AdminDeleteDoctorUseCase>();

builder.Services.AddTransient<IAdminViewSchedulesUseCase, AdminViewSchedulesUseCase>();
builder.Services.AddTransient<IAdminAddScheduleUseCase, AdminAddScheduleUseCase>();
builder.Services.AddTransient<IAdminDeleteScheduleUseCase, AdminDeleteScheduleUseCase>();
builder.Services.AddTransient<IAdminViewAllAppointmentsUseCase, AdminViewAllAppointmentsUseCase>();
// ===== K?T THÚC KH?I CODE C?N COPY =====
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
