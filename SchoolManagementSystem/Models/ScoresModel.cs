using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models;

public class ScoresModel{
    [Key] public int Id{ get; set; }
    public int CourseId{ get; set; }
    public int StudentId{ get; set; }
    public decimal Score{ get; set; }

    public StudentModel Student{ get; set; }
    public CourseModel Course{ get; set; }
}