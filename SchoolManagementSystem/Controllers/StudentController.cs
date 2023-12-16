using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers;

public class StudentController : Controller{
    private readonly AppDbContext _context;


    public StudentController(AppDbContext context){
        _context = context;
    }

    // GET
    public async Task<IActionResult> Index(){
        return View(await _context.Students.ToListAsync());
    }

    public async Task<IActionResult> Delete(int id){
        StudentModel? student = _context.Students.FirstOrDefault(m => m.Id == id);
        if (student != null){
            _context.Remove(student);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(System.Index));
    }

    public async Task<IActionResult> Edit(int? id){
        if (id == null){
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student == null){
            return NotFound();
        }

        return View(student);
    }


    // POST: Student/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,FirstName,LastName,Gender,BirthDay,PhoneNumber,Address")]
        StudentModel student){
        if (id != student.Id){
            return NotFound();
        }

        var dd = ModelState.IsValid;
        var sdcda = ModelState;

        if (ModelState.ErrorCount <= 1){
            try{
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!StudentExists(student.Id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        return View(student);
    }

    public async Task<IActionResult> Create(int? id){
        return View();
    }


    // POST: Student/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,FirstName,LastName,Gender,BirthDay,PhoneNumber,Address")]
        StudentModel student){
        if (ModelState.ErrorCount <= 1){
            try{
                _context.Add(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!StudentExists(student.Id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["error"] = "Invalid data";
        return View(student);
    }

    private bool StudentExists(int id){
        return _context.Students.Any(m => m.Id == id);
    }
}