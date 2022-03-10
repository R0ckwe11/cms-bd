using cms_bd.Data;
using DotNetEd.CoreAdmin;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:8100")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddCoreAdmin(new CoreAdminOptions()
{
    IgnoreEntityTypes = new List<Type>() { typeof(DateTime) }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.EnsureCreated();
    var databaseClean = DbInitializer.InitializeDefaultUser(context);
    // DbInitializer.AddImageDirectoryMetadata(context);
    if (databaseClean) DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();
