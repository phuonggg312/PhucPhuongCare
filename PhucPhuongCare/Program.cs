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
using PhucPhuongCare.UseCases.ViewModels;
using PhucPhuongCare.UseCases.PatientsUseCases;
using PhucPhuongCare.Data;
using Microsoft.AspNetCore.DataProtection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// ======= KẾT NỐI DATABASE =======
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("PhucPhuongCare.DataStore.EFCore")));

// ======= CẤU HÌNH IDENTITY + ROLE =======
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ======= COOKIE CHIA SẺ GIỮA CÁC MODULE =======
builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.Cookie.Name = ".PhucPhuongCare.SharedCookie";
        options.Cookie.Path = "/";
    });


// ======= BẢO VỆ DỮ LIỆU ĐĂNG NHẬP (DataProtection) =======
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"D:\temp-keys\"))
    .SetApplicationName("PhucPhuongCareShared");

// ======= ĐĂNG KÝ CÁC REPOSITORY =======
builder.Services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IDoctorScheduleRepository, DoctorScheduleRepository>();
builder.Services.AddTransient<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddTransient<IPatientProfileRepository, PatientProfileRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

// ======= ĐĂNG KÝ USE CASES (PATIENT + ADMIN) =======
builder.Services.AddTransient<IViewSpecialtiesUseCase, ViewSpecialtiesUseCase>();
builder.Services.AddTransient<IViewDoctorsBySpecialtyUseCase, ViewDoctorsBySpecialtyUseCase>();
builder.Services.AddTransient<IViewDoctorByIdUseCase, ViewDoctorByIdUseCase>();
builder.Services.AddTransient<IViewAvailableTimeSlotsUseCase, ViewAvailableTimeSlotsUseCase>();
builder.Services.AddTransient<IBookAppointmentUseCase, BookAppointmentUseCase>();
builder.Services.AddTransient<IViewMyAppointmentsUseCase, ViewMyAppointmentsUseCase>();
builder.Services.AddTransient<ICancelAppointmentUseCase, CancelAppointmentUseCase>();
builder.Services.AddTransient<IViewMyProfileUseCase, ViewMyProfileUseCase>();
builder.Services.AddTransient<ISaveMyProfileUseCase, SaveMyProfileUseCase>();

builder.Services.AddTransient<IAdminViewAllDoctorsUseCase, AdminViewAllDoctorsUseCase>();
builder.Services.AddTransient<IAdminAddDoctorUseCase, AdminAddDoctorUseCase>();
builder.Services.AddTransient<IAdminUpdateDoctorUseCase, AdminUpdateDoctorUseCase>();
builder.Services.AddTransient<IAdminDeleteDoctorUseCase, AdminDeleteDoctorUseCase>();
builder.Services.AddTransient<IAdminViewSchedulesUseCase, AdminViewSchedulesUseCase>();
builder.Services.AddTransient<IAdminAddScheduleUseCase, AdminAddScheduleUseCase>();
builder.Services.AddTransient<IAdminDeleteScheduleUseCase, AdminDeleteScheduleUseCase>();
builder.Services.AddTransient<IAdminViewAllAppointmentsUseCase, AdminViewAllAppointmentsUseCase>();
builder.Services.AddTransient<IAdminViewPatientInfoUseCase, AdminViewPatientInfoUseCase>();
builder.Services.AddTransient<IAdminCancelAppointmentUseCase, AdminCancelAppointmentUseCase>();
builder.Services.AddTransient<IAdminMarkAsCompletedUseCase, AdminMarkAsCompletedUseCase>();
builder.Services.AddTransient<IAdminViewAppointmentDetailUseCase, AdminViewAppointmentDetailUseCase>();
builder.Services.AddTransient<IGenerateTimeSlotsUseCase, GenerateTimeSlotsUseCase>();


// ======= CẤU HÌNH BLAZOR =======
//builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Pages/Admin", "Admin");
    options.Conventions.AllowAnonymousToPage("/Identity/Account/Login");
});

var app = builder.Build();

// ======= SEED ROLE VÀ ADMIN =======
await SeedDatabase(app);

// ======= CONFIGURE MIDDLEWARE =======
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();


// ======= HÀM SEED ROLE VÀ ADMIN =======
async Task SeedDatabase(WebApplication webApp)
{
    using (var scope = webApp.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Patient" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@phucphuongcare.com");
            if (adminUser == null)
            {
                var newAdmin = new IdentityUser
                {
                    UserName = "admin@phucphuongcare.com",
                    Email = "admin@phucphuongcare.com",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(newAdmin, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
}
