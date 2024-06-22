using asd123.Helpers;
using asd123.Middlewares;
using asd123.Model;
using asd123.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerConfig();
string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.DatabaseConnection(connectionString);
builder.Services.AddScoped<IKhoaService, KhoaService>();
builder.Services.AddScoped<IChuyenNganh, ChuyenNganhService>();
builder.Services.AddScoped<IDiem, DiemService>();
builder.Services.AddScoped<ILop, LopService>();
builder.Services.AddScoped<IMonHoc, MonHocService>();
builder.Services.AddScoped<ISinhVien, SinhVienService>();
builder.Services.AddScoped<IThongKeService, ThongKeService>();
builder.Services.AddAutoMapper(typeof(Program)); 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
// Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

var app = builder.Build();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});


/***Configure the HTTP request pipeline.***/
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Base Core V3");
    });
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production
    // scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
