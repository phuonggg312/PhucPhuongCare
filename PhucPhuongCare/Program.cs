using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.DataStore.EFCore;
using PhucPhuongCare.DataStore.EFCore.Repositories;
using PhucPhuongCare.UseCases.PluginInterfaces;
using PhucPhuongCare.UseCases.SpecialtiesUseCases;
using PhucPhuongCare.UseCases.DoctorsUseCases;
using PhucPhuongCare.UseCases.TimeSlotsUseCases;
using PhucPhuongCare.UseCases.AppointmentsUseCases;
using PhucPhuongCare.UseCases.PatientProfilesUseCases;
using Microsoft.AspNetCore.DataProtection;
using PhucPhuongCare.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Cấu hình AppDbContext và chỉ định Migrations Assembly
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("PhucPhuongCare.DataStore.EFCore")));

// Cấu hình ApplicationDbContext và chỉ định Migrations Assembly
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("PhucPhuongCare.DataStore.EFCore")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Cấu hình Identity và Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false) // << SỬA Ở ĐÂY
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Cấu hình để chia sẻ cookie giữa 2 ứng dụng
builder.Services.AddAuthentication()
    .AddCookie(options => {
        options.Cookie.Name = ".PhucPhuongCare.SharedCookie";
        options.Cookie.Path = "/";
    });

// Cấu hình Data Protection để 2 app có thể giải mã cookie của nhau
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"D:\temp-keys\"))
    .SetApplicationName("PhucPhuongCareShared");


// Đăng ký Repositories
builder.Services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IDoctorScheduleRepository, DoctorScheduleRepository>();
builder.Services.AddTransient<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddTransient<IPatientProfileRepository, PatientProfileRepository>();

// Đăng ký Use Cases
builder.Services.AddTransient<IViewSpecialtiesUseCase, ViewSpecialtiesUseCase>();
builder.Services.AddTransient<IViewDoctorsBySpecialtyUseCase, ViewDoctorsBySpecialtyUseCase>();
builder.Services.AddTransient<IViewDoctorByIdUseCase, ViewDoctorByIdUseCase>();
builder.Services.AddTransient<IViewAvailableTimeSlotsUseCase, ViewAvailableTimeSlotsUseCase>();
builder.Services.AddTransient<IGenerateTimeSlotsUseCase, GenerateTimeSlotsUseCase>();
builder.Services.AddTransient<IBookAppointmentUseCase, BookAppointmentUseCase>();
builder.Services.AddTransient<IViewMyAppointmentsUseCase, ViewMyAppointmentsUseCase>();
builder.Services.AddTransient<ICancelAppointmentUseCase, CancelAppointmentUseCase>();
builder.Services.AddTransient<IViewMyProfileUseCase, ViewMyProfileUseCase>();
builder.Services.AddTransient<ISaveMyProfileUseCase, SaveMyProfileUseCase>();
builder.Services.AddTransient<IAdminViewAppointmentDetailUseCase, AdminViewAppointmentDetailUseCase>();


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


var app = builder.Build();

// =============================================
// GỌI HÀM TẠO ROLE VÀ ADMIN SAU KHI BUILD APP
// =============================================
await SeedDatabase(app);


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Thứ tự hai dòng này rất quan trọng
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


// =============================================
// HÀM HELPER ĐỂ TẠO ROLE VÀ ADMIN
// =============================================
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
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@phucphuongcare.com");
            if (adminUser == null)
            {
                var newAdmin = new IdentityUser()
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