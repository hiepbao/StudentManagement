using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class Grade
{
    [Key]
    public int GradeId { get; set; }

    [StringLength(50)]
    public string? GradeName { get; set; }

    [InverseProperty("Grade")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
