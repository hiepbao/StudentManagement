using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

[Table("Class")]
public partial class Class
{
    [Key]
    public int ClassId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ClassName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? SchoolYear { get; set; }

    public int? GradeId { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();

    [InverseProperty("Class")]
    public virtual ICollection<Diligence> Diligences { get; set; } = new List<Diligence>();

    [ForeignKey("GradeId")]
    [InverseProperty("Classes")]
    public virtual Grade? Grade { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    [InverseProperty("Class")]
    public virtual ICollection<StudyPoint> StudyPoints { get; set; } = new List<StudyPoint>();
}
