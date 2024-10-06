using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

[Table("Subject")]
public partial class Subject
{
    [Key]
    public int SubjectId { get; set; }

    [StringLength(50)]
    public string? SubjectName { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<StudyPoint> StudyPoints { get; set; } = new List<StudyPoint>();

    [InverseProperty("Subject")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
