using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Frency.Base;
using Frency.DataAccess;
using Frency.DataAccess.Models;
using Frency.DataAccess.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverterHelper());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.UseSnakeCaseNamingConvention();
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Frency API",
        Description = "API untuk aplikasi Frency",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("https://ismilelab-telu.com", "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();

    });
});

var key = Encoding.ASCII.GetBytes("3a97556f-c07f-4f5a-8bc6-28711d4922e9");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AbstractModelValidator<BaseModel>>();

//builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IFileProvider>(
    new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t =>
        !t.IsInterface &&
        t != typeof(BaseService<>) &&
        t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseService<>))
    )
    .ToList().ForEach(assignedTypes =>
    {
        var serviceType = assignedTypes.GetInterfaces().First(i => !i.IsGenericType);
        builder.Services.AddScoped(serviceType, assignedTypes);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseAuthentication();

app.UseCors();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    opt.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.MapControllers();
}
else
{
    app.MapControllers().RequireAuthorization();
}

app.Run();
