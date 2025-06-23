////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.

////builder.Services.AddControllers();
////// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

////app.UseHttpsRedirection();

////app.UseAuthorization();

////app.MapControllers();

////app.Run();
//using Microsoft.EntityFrameworkCore;
//using ShoppingListAPI.Models;
//using ShoppingListAPI.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// ����� Entity Framework �� SQLite
//builder.Services.AddDbContext<ShoppingDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//// ����� Services
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IShoppingListService, ShoppingListService>();

//// ����� CORS ������
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        builder =>
//        {
//            builder
//                .AllowAnyOrigin()
//                .AllowAnyMethod()
//                .AllowAnyHeader();
//        });
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// ����� CORS
//app.UseCors("AllowAll");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
using Microsoft.EntityFrameworkCore;
using ShoppingListAPI.Models;
using ShoppingListAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ����� Entity Framework �� SQLite
builder.Services.AddDbContext<ShoppingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ����� Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IShoppingListService, ShoppingListService>();

// ����� CORS ������
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// ����� ��� ������� ��������
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShoppingDbContext>();
    context.Database.EnsureCreated();

    // ����� ������ �������� �� ���� ���
    try
    {
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(new[]
            {
            new Category { Name = "Vegetables" },
            new Category { Name = "Dairy Products" },
            new Category { Name = "Beverages" },
            new Category { Name = "Meat & Fish" },
            new Category { Name = "Snacks" }
        });

            context.SaveChanges();
        }
    }
    catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE constraint") == true)
    {
        // �� ��������� ��� ������ - ���� ������
        Console.WriteLine("Categories already exist, skipping seed data...");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ����� CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
