using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MVCMovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MVCMovieContext") ?? throw new InvalidOperationException("Connection string 'MVCMovieContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();