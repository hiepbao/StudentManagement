using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class Parent
{
    [Key]
    public int ParentId { get; set; }

    public int? UserId { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    [ForeignKey("UserId")]
    [InverseProperty("Parents")]
    public virtual User? User { get; set; }
}
