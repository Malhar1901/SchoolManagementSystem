﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Controllers;

public class HomeController : Controller{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;


    public HomeController(ILogger<HomeController> logger, AppDbContext context){
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index(){
        return View(await _context.Students.ToListAsync());
    }

    public async Task<IActionResult> Course(){
        return View();
    }

    public async Task<IActionResult> Score(){
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}