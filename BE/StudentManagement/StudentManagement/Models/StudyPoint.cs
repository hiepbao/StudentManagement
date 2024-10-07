using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class StudyPoint
{
    [Key]
    public int PointId { get; set; }

    public int? StudentId { get; set; }

    public int? ClassId { get; set; }

    [Column(TypeName = "decimal(3, 1)")]
    public decimal? GradeFactor1 { get; set; }

    [Column(TypeName = "decimal(3, 1)")]
    public decimal? GradeFactor2 { get; set; }

    [Column(TypeName = "decimal(3, 1)")]
    public decimal? GradeFactor3 { get; set; }

    [Column(TypeName = "decimal(3, 1)")]
    public decimal? AveragePoint { get; set; }

    public int? TeacherId { get; set; }

    public int? SubjectId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? GradeEntryDate { get; set; }

    public int? SemesterId { get; set; }

    [StringLength(10)]
    public string? AcademicAbility { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("StudyPoints")]
    public virtual Class? Class { get; set; }

    [ForeignKey("SemesterId")]
    [InverseProperty("StudyPoints")]
    public virtual Semester? Semester { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudyPoints")]
    public virtual Student? Student { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("StudyPoints")]
    public virtual Subject? Subject { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("StudyPoints")]
    public virtual Teacher? Teacher { get; set; }
}
