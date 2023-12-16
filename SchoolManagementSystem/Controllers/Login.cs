using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NuGet.ProjectModel;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using Microsoft.AspNetCore.Identity;


namespace SchoolManagementSystem.Controllers;

public class Login : Controller{
    private readonly AppDbContext _context;

    public Login(AppDbContext context){
        _context = context;
    }

    // GET
    public IActionResult Index(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(String username, String password){
        IPasswordHasher<AdminUserModel> _passwordHasher = new PasswordHasher<AdminUserModel>();

        AdminUserModel? user = _context.AdminUsers.FirstOrDefault(w =>
            w.UserName == username && password == "123456789");

        if (user == null){
            ViewBag.Messege = "Invalid credentials";
            return View();
        }
        return Redirect("/Home");
    }
}