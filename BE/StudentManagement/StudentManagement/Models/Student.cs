using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

[Table("Student")]
public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    public int? UserId { get; set; }

    public int? ClassId { get; set; }

    public int? ParentId { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("Students")]
    public virtual Class? Class { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<Diligence> Diligences { get; set; } = new List<Diligence>();

    [ForeignKey("ParentId")]
    [InverseProperty("Students")]
    public virtual Parent? Parent { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<StudyPoint> StudyPoints { get; set; } = new List<StudyPoint>();

    [ForeignKey("UserId")]
    [InverseProperty("Students")]
    public virtual User? User { get; set; }
}
