using Microsoft.EntityFrameworkCore;
using CesCmsDashboard.Data;
using CesCmsDashboard.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryDb");
if (builder.Environment.IsDevelopment() && useInMemory) {
    builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("CesLocalDb"));
} else {
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}

var app = builder.Build();

if (app.Environment.IsDevelopment() && app.Configuration.GetValue<bool>("UseInMemoryDb")) {
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    if (!db.Faqs.Any()) {
        db.Faqs.Add(new Faq { Id = Guid.NewGuid(), Question = "Local Mock Q1?", Answer = "Local Mock A1", IsPublished = true });
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
