using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhucPhuongCare.Data;
using PhucPhuongCare.DataStore.EFCore;
using PhucPhuongCare.DataStore.EFCore.Repositories;
using PhucPhuongCare.UseCases.PluginInterfaces;
using PhucPhuongCare.UseCases.SpecialtiesUseCases;
using PhucPhuongCare.UseCases.DoctorsUseCases;
using PhucPhuongCare.UseCases.TimeSlotsUseCases;
using PhucPhuongCare.UseCases.AppointmentsUseCases;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Đăng ký AppDbContext và chỉ định Migrations Assembly
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("PhucPhuongCare.DataStore.EFCore")));

// Đăng ký ApplicationDbContext và chỉ định Migrations Assembly
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("PhucPhuongCare.DataStore.EFCore")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // << THÊM DÒNG NÀY
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Đăng ký Repositories
builder.Services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IDoctorScheduleRepository, DoctorScheduleRepository>();
builder.Services.AddTransient<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
// Đăng ký Use Cases

builder.Services.AddTransient<IViewSpecialtiesUseCase, ViewSpecialtiesUseCase>();
builder.Services.AddTransient<IViewDoctorsBySpecialtyUseCase, ViewDoctorsBySpecialtyUseCase>();
// Đăng ký Use Cases
builder.Services.AddTransient<IViewSpecialtiesUseCase, ViewSpecialtiesUseCase>();
builder.Services.AddTransient<IViewDoctorsBySpecialtyUseCase, ViewDoctorsBySpecialtyUseCase>();
builder.Services.AddTransient<IViewDoctorByIdUseCase, ViewDoctorByIdUseCase>(); // << Dòng mới
builder.Services.AddTransient<IViewAvailableTimeSlotsUseCase, ViewAvailableTimeSlotsUseCase>();
builder.Services.AddTransient<IGenerateTimeSlotsUseCase, GenerateTimeSlotsUseCase>();
builder.Services.AddTransient<IBookAppointmentUseCase, BookAppointmentUseCase>();
builder.Services.AddTransient<IViewMyAppointmentsUseCase, ViewMyAppointmentsUseCase>();
builder.Services.AddTransient<ICancelAppointmentUseCase, CancelAppointmentUseCase>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();
// =============================================
// ===== BẮT ĐẦU KHỐI CODE TẠO ROLE VÀ ADMIN =====
// =============================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Tạo các Role "Admin" và "Patient" nếu chúng chưa tồn tại
        string[] roleNames = { "Admin", "Patient" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Tạo một tài khoản Admin mặc định nếu nó chưa tồn tại
        var adminUser = await userManager.FindByEmailAsync("admin@phucphuongcare.com");
        if (adminUser == null)
        {
            var newAdmin = new IdentityUser()
            {
                UserName = "admin@phucphuongcare.com",
                Email = "admin@phucphuongcare.com",
                EmailConfirmed = true
            };
            // Mật khẩu cho tài khoản admin là: Password123!
            var result = await userManager.CreateAsync(newAdmin, "Password123!");
            if (result.Succeeded)
            {
                // Gán vai trò "Admin" cho tài khoản vừa tạo
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
// ===== KẾT THÚC KHỐI CODE TẠO ROLE VÀ ADMIN =====

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();