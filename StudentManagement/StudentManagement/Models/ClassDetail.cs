using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

[Table("ClassDetail")]
public partial class ClassDetail
{
    [Key]
    public int ClassDetailId { get; set; }

    public int? ClassId { get; set; }

    public int? TeacherId { get; set; }

    [StringLength(50)]
    public string? Subject { get; set; }

    [StringLength(100)]
    public string? Schedule { get; set; }

    public int? HomeroomTeacherId { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("ClassDetails")]
    public virtual Class? Class { get; set; }

    [ForeignKey("HomeroomTeacherId")]
    [InverseProperty("ClassDetails")]
    public virtual HomeroomTeacher? HomeroomTeacher { get; set; }

    [ForeignKey("HomeroomTeacherId")]
    [InverseProperty("ClassDetails")]
    public virtual Teacher? HomeroomTeacherNavigation { get; set; }
}
