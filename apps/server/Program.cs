using profisysApp.Data;
using profisysApp.Services;
using profisysApp.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using profisysApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var appSettings = new AppSettings();
builder.Services.AddSingleton(appSettings);

// Database
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDocumentsRepository, DocumentsRepository>();
builder.Services.AddScoped<IItemsRepository, ItemsRepository>();

// Services
builder.Services.AddScoped<DataImportService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DocumentsService>();
builder.Services.AddScoped<ItemsService>();
builder.Services.AddSingleton<AuditService>();

// Seeder
builder.Services.AddHostedService<DatabaseSeeder>();

// Mapper
builder.Services.AddAutoMapper(typeof(Program));

// Controllers & JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(appSettings.CLIENT_URL_ADDRESS) 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Authentication & Authorization
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true, // To MUSI być true!
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build app
var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();