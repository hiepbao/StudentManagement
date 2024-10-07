using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class Teacher
{
    [Key]
    public int TeacherId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    public int? SubjectId { get; set; }

    [InverseProperty("Teacher")]
    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();

    [InverseProperty("Teacher")]
    public virtual ICollection<Diligence> Diligences { get; set; } = new List<Diligence>();

    [InverseProperty("Teacher")]
    public virtual ICollection<HomeroomTeacher> HomeroomTeachers { get; set; } = new List<HomeroomTeacher>();

    [InverseProperty("Teacher")]
    public virtual ICollection<StudyPoint> StudyPoints { get; set; } = new List<StudyPoint>();

    [ForeignKey("SubjectId")]
    [InverseProperty("Teachers")]
    public virtual Subject? Subject { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Teachers")]
    public virtual User? User { get; set; }
}
