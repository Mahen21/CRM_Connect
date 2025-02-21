using CRMConnect.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure SQL Server and ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register HttpClient for dependency injection
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed exception page in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Strict HTTP headers for production environments
}

app.UseStaticFiles(); // Serve static files (like JS, CSS)

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Set up routing for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run(); // Run the application
