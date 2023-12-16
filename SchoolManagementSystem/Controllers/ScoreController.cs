using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers;

public class ScoreController : Controller{

    private readonly AppDbContext _context;


    public ScoreController(AppDbContext context){
        _context = context;
    }

    // GET
    public IActionResult Index(){
        return RedirectToAction(nameof(Student));
    }

    public async Task<IActionResult> Student(){
        return View(await _context.Students.ToListAsync());
    }

    public async Task<IActionResult> Course(){
        return View(await _context.Courses.ToListAsync());
    }

    public async Task<IActionResult> ScoreStudent(int? studentId){
        if (studentId == null)
            return NotFound();
        var data = _context.Scores.Where(m => m.StudentId == studentId).Include(c => c.Course).Include(c => c.Student);
        return View(data);
    }

    public async Task<IActionResult> ScoreCourse(int? courseId){
        if (courseId == null){
            return NotFound();
        }

        var data = _context.Scores.Where(m => m.CourseId == courseId).Include(c => c.Course).Include(c => c.Student);
        return View(data);
    }

    public async Task<IActionResult> Add(){
        ViewData["students"] = _context.Students.ToListAsync().Result;
        ViewData["courses"] = _context.Courses.ToListAsync().Result;

        return View();
    }

    // POST: Score/Add
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(
        [Bind("Id,CourseId,StudentId,Score")] ScoresModel scoreModel){
        if (ModelState.ErrorCount <= 2){
            try{
                _context.Add(scoreModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!ScoreExists(scoreModel.Id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["error"] = "Invalid data";

        ViewData["students"] = _context.Students.ToListAsync().Result;
        ViewData["courses"] = _context.Courses.ToListAsync().Result;

        return View(scoreModel);
    }

    public async Task<IActionResult> Edit(int? id){
        if (id == null){
            return NotFound();
        }

        var scores = await _context.Scores.FindAsync(id);
        if (scores == null){
            return NotFound();
        }

        return View(scores);
    }

    // POST: Scores/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,CourseId,StudentId,Score")] ScoresModel scoreModel){
        if (id != scoreModel.Id){
            return NotFound();
        }
        

        if (ModelState.ErrorCount <= 2){
            try{
                _context.Update(scoreModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!ScoreExists(scoreModel.Id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        return View(scoreModel);
    }

    public async Task<IActionResult> Delete(int? id){
        ScoresModel? scores = _context.Scores.FirstOrDefault(m => m.Id == id);
        if (scores != null){
            _context.Remove(scores);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ScoreExists(int id){
        return _context.Scores.Any(m => m.Id == id);
    }
}