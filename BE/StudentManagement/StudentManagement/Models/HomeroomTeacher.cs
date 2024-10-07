using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

[Table("HomeroomTeacher")]
public partial class HomeroomTeacher
{
    [Key]
    public int HomeroomTeacherId { get; set; }

    [Column("TeacherID")]
    public int? TeacherId { get; set; }

    [InverseProperty("HomeroomTeacher")]
    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();

    [ForeignKey("TeacherId")]
    [InverseProperty("HomeroomTeachers")]
    public virtual Teacher? Teacher { get; set; }
}
