using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class Semester
{
    [Key]
    public int SemesterId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? SemesterName { get; set; }

    [Precision(1)]
    public DateTime? StartSemester { get; set; }

    [Precision(1)]
    public DateTime? EndSemester { get; set; }

    [InverseProperty("Semester")]
    public virtual ICollection<Diligence> Diligences { get; set; } = new List<Diligence>();

    [InverseProperty("Semester")]
    public virtual ICollection<StudyPoint> StudyPoints { get; set; } = new List<StudyPoint>();
}
