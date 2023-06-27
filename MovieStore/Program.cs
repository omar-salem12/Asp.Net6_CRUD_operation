using Microsoft.EntityFrameworkCore;
using MovieStore.Data;
using MovieStore.Models.Repository;
using NToastNotify;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = false,
    PositionClass =ToastPositions.BottomCenter

});
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddDbContext<MovieDbContext>(option => option.
                 UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));

var app = builder.Build();
app.UseNToastNotify();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
