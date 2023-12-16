using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models;

public class CourseModel{
    [Key] public int Id{ get; set; }
    public string? Name{ get; set; }
    public int Duration{ get; set; }
    public string? Description{ get; set; }

    public ICollection<ScoresModel> Scores{ get; set; }
}