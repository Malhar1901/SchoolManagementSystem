namespace SchoolManagementSystem.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

public class AppDbContext : DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ScoresModel>()
            .HasOne(c => c.Student)
            .WithMany(u => u.Scores)
            .HasForeignKey(d => d.StudentId)
            .IsRequired();
        
        modelBuilder.Entity<ScoresModel>()
            .HasOne(c => c.Course)
            .WithMany(u => u.Scores)
            .HasForeignKey(d => d.CourseId)
            .IsRequired();
        
    }

    public DbSet<StudentModel> Students{ get; set; }
    public DbSet<ScoresModel> Scores{ get; set; }
    public DbSet<CourseModel> Courses{ get; set; }
    public DbSet<AdminUserModel> AdminUsers{ get; set; }
}