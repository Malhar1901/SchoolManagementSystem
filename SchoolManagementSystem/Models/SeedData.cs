using Microsoft.AspNetCore.Identity;
using SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class SeedData{
    public static void Initialize(IServiceProvider serviceProvider){
        using (var context =
               new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>())){
            if (!context.Database.EnsureCreated()){
                return; // exit seed if database is not created
            }

            context.Database.Migrate();

            SeedStudents(context);
            SeedCourses(context);
            SeedAdminUser(context);
            SeedScores(context);
        }
    }

    private static void SeedScores(AppDbContext context){
        context.Scores.AddRange(new ScoresModel{
                Id = 1,
                CourseId = 1,
                StudentId = 1,
                Score = 78.9M,
            },
            new ScoresModel{
                Id = 2,
                CourseId = 2,
                StudentId = 1,
                Score = 90.9M,
            },
            new ScoresModel{
                Id = 4,
                CourseId = 3,
                StudentId = 1,
                Score = 60.7M,
            },
            new ScoresModel{
                Id = 5,
                CourseId = 1,
                StudentId = 2,
                Score = 76.8M,
            },
            new ScoresModel{
                Id = 6,
                CourseId = 3,
                StudentId = 2,
                Score = 34.2M,
            },
            new ScoresModel{
                Id = 7,
                CourseId = 2,
                StudentId = 3,
                Score = 56.8M,
            },
            new ScoresModel{
                Id = 8,
                CourseId = 3,
                StudentId = 3,
                Score = 54.1M,
            }
        );

        context.SaveChanges();
    }

    private static void SeedAdminUser(AppDbContext context){
        IPasswordHasher<AdminUserModel> _passwordHasher = new PasswordHasher<AdminUserModel>();

        AdminUserModel user = new AdminUserModel();
        Guid guid = Guid.NewGuid();
        user.Id = guid.ToString();
        user.UserName = "Admin";
        user.Email = "admin@admin.com";
        user.NormalizedUserName = "admin@admin.com";

        context.AdminUsers.Add(user);

        var hashedPassword = _passwordHasher.HashPassword(user, "123456789");
        user.SecurityStamp = Guid.NewGuid().ToString();
        user.PasswordHash = hashedPassword;

        context.SaveChanges();
    }

    private static void SeedStudents(AppDbContext context){
        context.Students.AddRange(
            new StudentModel{
                Address = "london",
                BirthDay = DateTime.Parse("12-06-2001"),
                FirstName = "Yui",
                LastName = "Hasegawa",
                Gender = "female",
                PhoneNumber = "7467342394",
                Id = 2
            },
            new StudentModel{
                Address = "Texas",
                BirthDay = DateTime.Parse("01-07-2002"),
                FirstName = "John",
                LastName = "Doe",
                Gender = "male",
                PhoneNumber = "66523238544",
                Id = 3
            },
            new StudentModel{
                Address = "Germany",
                BirthDay = DateTime.Parse("11-07-1999"),
                FirstName = "Simon",
                LastName = "Hopper",
                Gender = "male",
                PhoneNumber = "762332686465",
                Id = 1
            }
        );

        context.SaveChanges();
    }

    private static void SeedCourses(AppDbContext context){
        context.Courses.AddRange(
            new CourseModel{
                Id = 1,
                Description = "A Fundamental python course",
                Duration = 36,
                Name = "Python for beggineers"
            },
            new CourseModel{
                Id = 2,
                Description = "An Advanced Java course",
                Duration = 80,
                Name = "Java SE/Java EE Core"
            },
            new CourseModel{
                Id = 3,
                Description = "An Intermediate C# course",
                Duration = 50,
                Name = "C# Fundamentals"
            }
        );

        context.SaveChanges();
    }
}