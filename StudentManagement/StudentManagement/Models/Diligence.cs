using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

[Table("Diligence")]
public partial class Diligence
{
    [Key]
    public int DiligenceId { get; set; }

    public int? StudentId { get; set; }

    public int? ClassId { get; set; }

    public int? TeacherId { get; set; }

    public int? SemesterId { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? ExcusedAbsence { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? UnexcusedAbsence { get; set; }

    [StringLength(10)]
    public string? AttendanceRating { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("Diligences")]
    public virtual Class? Class { get; set; }

    [ForeignKey("SemesterId")]
    [InverseProperty("Diligences")]
    public virtual Semester? Semester { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Diligences")]
    public virtual Student? Student { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("Diligences")]
    public virtual Teacher? Teacher { get; set; }
}
