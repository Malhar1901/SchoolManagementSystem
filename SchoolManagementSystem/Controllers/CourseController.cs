using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers;

public class CourseController : Controller{
    private readonly AppDbContext _context;


    public CourseController(AppDbContext context){
        _context = context;
    }

    // GET
    public async Task<IActionResult> Index(){
        return View(await _context.Courses.ToListAsync());
    }

    public async Task<IActionResult> Delete(int id){
        CourseModel? course = _context.Courses.FirstOrDefault(m => m.Id == id);
        if (course != null){
            _context.Remove(course);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(System.Index));
    }

    public async Task<IActionResult> Edit(int? id){
        if (id == null){
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);
        if (course == null){
            return NotFound();
        }

        return View(course);
    }


    // POST: course/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,Duration,Description")] CourseModel course){
        if (id != course.Id){
            return NotFound();
        }

        var dd = ModelState.IsValid;
        var sdcda = ModelState;

        if (ModelState.ErrorCount <= 1){
            try{
                _context.Update(course);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!CourseExists(course.Id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        return View(course);
    }

    public async Task<IActionResult> Create(){
        return View();
    }


    // POST: course/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Duration,Description")] CourseModel course){
        if (ModelState.ErrorCount <= 1){
            try{
                _context.Add(course);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!CourseExists(course.Id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["error"] = "Invalid data";
        return View(course);
    }

    private bool CourseExists(int id){
        return _context.Courses.Any(m => m.Id == id);
    }
}