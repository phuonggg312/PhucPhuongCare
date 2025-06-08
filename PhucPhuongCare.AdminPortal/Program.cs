using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.DataStore.EFCore;
using PhucPhuongCare.DataStore.EFCore.Repositories;
using PhucPhuongCare.UseCases.PluginInterfaces;
using PhucPhuongCare.UseCases.SpecialtiesUseCases;
using PhucPhuongCare.UseCases.DoctorsUseCases;
using PhucPhuongCare.UseCases.TimeSlotsUseCases;
using PhucPhuongCare.UseCases.AppointmentsUseCases;
using PhucPhuongCare.UseCases.SchedulesUseCases;
using PhucPhuongCare.UseCases.PatientProfilesUseCases;
using Microsoft.AspNetCore.DataProtection;
using PhucPhuongCare.UseCases.ViewModels;
using PhucPhuongCare.UseCases.PatientsUseCases;
using PhucPhuongCare.Data;

var builder = WebApplication.CreateBuilder(args);

// L?y chu?i k?t n?i
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// C?u h�nh DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("PhucPhuongCare.DataStore.EFCore")));

// C?u h�nh Identity v� Roles
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// C?u h�nh ?? chia s? cookie ??ng nh?p
builder.Services.AddAuthentication()
    .AddCookie(options => {
        options.Cookie.Name = ".PhucPhuongCare.SharedCookie";
        options.Cookie.Path = "/";
    });

// C?u h�nh Data Protection ?? 2 app c� th? gi?i m� cookie c?a nhau
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"D:\temp-keys\"))
    .SetApplicationName("PhucPhuongCareShared");

// ??ng k� Repositories
builder.Services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IDoctorScheduleRepository, DoctorScheduleRepository>();
builder.Services.AddTransient<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddTransient<IPatientProfileRepository, PatientProfileRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();


// ??ng k� Use Cases
builder.Services.AddTransient<IViewSpecialtiesUseCase, ViewSpecialtiesUseCase>();
builder.Services.AddTransient<IViewDoctorsBySpecialtyUseCase, ViewDoctorsBySpecialtyUseCase>();
builder.Services.AddTransient<IViewDoctorByIdUseCase, ViewDoctorByIdUseCase>();
builder.Services.AddTransient<IAdminViewAllDoctorsUseCase, AdminViewAllDoctorsUseCase>();
builder.Services.AddTransient<IAdminAddDoctorUseCase, AdminAddDoctorUseCase>();
builder.Services.AddTransient<IAdminUpdateDoctorUseCase, AdminUpdateDoctorUseCase>();
builder.Services.AddTransient<IAdminDeleteDoctorUseCase, AdminDeleteDoctorUseCase>();
builder.Services.AddTransient<IAdminViewSchedulesUseCase, AdminViewSchedulesUseCase>();
builder.Services.AddTransient<IAdminAddScheduleUseCase, AdminAddScheduleUseCase>();
builder.Services.AddTransient<IAdminDeleteScheduleUseCase, AdminDeleteScheduleUseCase>();
builder.Services.AddTransient<IGenerateTimeSlotsUseCase, GenerateTimeSlotsUseCase>();
builder.Services.AddTransient<IAdminViewAllAppointmentsUseCase, AdminViewAllAppointmentsUseCase>();
builder.Services.AddTransient<IAdminViewPatientInfoUseCase, AdminViewPatientInfoUseCase>();


// C�c d?ch v? m?c ??nh c?a Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Th�m 2 d�ng quan tr?ng n�y v�o
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();