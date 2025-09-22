using GymLogger.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymLogger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string input = """
                SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note
                1,LegDay,2,Squat,105,10,3,02.09.2025 9:00:27 docela v pohode drepiky
                1,LegDay,13,50,12,3,02.09.2025 9:00:50,najs nebo najs
                1,LegDay,6,Bulgarian Split Squat,125,10,3,02.09.2025 9:01:36,
                1,LegDay,13,Leg Raises,0,0,0,02.09.2025 9:26:59,
                1,LegDay,1,Bench Press,10,2,2,Tohle neni cas,
                3,Nohy,2,Squat,100,6,3,14.09.2025 13:27:07,
                3,Nohy,7,Romanian Deadlift,60,NAN,3,14.09.2025 13:27:45,
                3,Nohy,10,Calf Raises,Nan,12,6,14.09.2025 13:28:01,
                """;
            CSVReader reader = new CSVReader(new StringReader(input));
            //Act
            var result = reader.GetKeys();
            string actual1 = string.Join(",", result!);
            result = reader.GetLine(out _); // should be null, because of missing separator
            var result1 = reader.GetLine(out _); // null, missing exercise name
            var result2 = reader.GetLine(out _); // should be okay
            var result3 = reader.GetLine(out _); // okay, zero values for weight, reps and sets
            var result4 = reader.GetLine(out _); // should be null, incorrect time format
            var result5 = reader.GetLine(out _); // okay
            var result6 = reader.GetLine(out _); // null, not a number in rep's place
            var result7 = reader.GetLine(out _); // null, not a double in weight's place

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            
            // not functioning because Facebook needs business verification and data deletion instructions,
            // the problem is in business verification
            //builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"]!;
            //    facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"]!;
            //});

            builder.Services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
            });


            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/");
                options.Conventions.AllowAnonymousToPage("/Index");
                options.Conventions.AllowAnonymousToFolder("/Identity/Account");
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
