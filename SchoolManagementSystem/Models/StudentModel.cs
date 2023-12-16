using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models;

public class StudentModel{
    [Key] public int Id{ get; set; }

    public string? FirstName{ get; set; }
    public string? LastName{ get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime BirthDay{ get; set; }

    public string? Gender{ get; set; }
    public string? PhoneNumber{ get; set; }
    public string? Address{ get; set; }

    public ICollection<ScoresModel> Scores{ get; set; }
}