using MusicBusniess;
using MusicCenterAPI.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MusicCenterAPI.Data;
using MusicBusniess.Data;

namespace MusicCenterAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IMusicCenterAPI, MusicCenterAPI>();

            builder.Services.Configure<MusicCenterAPI>(builder.Configuration.GetSection("AppSettings"));
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            string secretKey = jwtSettings["Key"];
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            MusicCenterAPI api = new MusicCenterAPI(configuration);
            api.procedure.createProcedure();


            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:7105", "https://3rl1l9sm40pb.share.zrok.io", "https://localhost:7089") // Chỉ định nguồn cụ thể
                   .AllowAnyMethod()   // Cho phép mọi phương thức
                   .AllowAnyHeader()   // Cho phép mọi header
                   .AllowCredentials();
                    });
            });
            IConfiguration jwtConfig = builder.Configuration;
            var appSettingsSection = jwtConfig.GetSection("JwtSettings");
            builder.Services.Configure<AppSetting>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.Key);
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


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();      

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}