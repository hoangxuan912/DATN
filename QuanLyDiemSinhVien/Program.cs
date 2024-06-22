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
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig!);
var roles = new[] { "admin", "mod", "member", "user" };

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerConfig();
string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.DatabaseConnection(connectionString);

builder.Services.AddDefaultIdentity<User>(options => {
        options.SignIn.RequireConfirmedAccount = true;
        //options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
    }).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// builder.Services.AddScoped<IExcelDataService, ExcelDataService>();
builder.Services.AddScoped<IEmailservice, Emailservice>();
builder.Services.AddScoped<IKhoaService, KhoaService>();
builder.Services.AddScoped<IChuyenNganh, ChuyenNganhService>();
builder.Services.AddScoped<IDiem, DiemService>();
builder.Services.AddScoped<ILop, LopService>();
builder.Services.AddScoped<IMonHoc, MonHocService>();
builder.Services.AddScoped<ISinhVien, SinhVienService>();
builder.Services.AddScoped<IThongKeService, ThongKeService>();
builder.Services.AddAutoMapper(typeof(Program)); 
builder.Services.AddSingleton(emailConfig!);
/***My Own Services***/
builder.Services.AddScoped<AuthorisationServices>();

/***Policies builder***/
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminAccess", policy => policy.RequireRole(roles[0]))
    .AddPolicy("ModsAccess", policy => policy.RequireAssertion(context => context.User.IsInRole(roles[0]) ||
                                                                          context.User.IsInRole(roles[1])))
    .AddPolicy("MemberAcces", policy => policy.RequireAssertion(context => context.User.IsInRole(roles[0]) ||
                                                                          context.User.IsInRole(roles[1]) ||
                                                                          context.User.IsInRole(roles[3])));
/***Injecting time in the recovery token***/
builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(24));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
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
app.MapIdentityApi<User>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

/***Adding the role we want in th/Errore database***/
/***On Comments this sections when your connections string will be set***/
/*
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}
*/
app.Run();
