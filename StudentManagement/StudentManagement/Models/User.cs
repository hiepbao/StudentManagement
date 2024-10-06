using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? Gender { get; set; }

    [StringLength(50)]
    public string? Address { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string? Mail { get; set; }

    [StringLength(100)]
    public string? Password { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Birthday { get; set; }

    [InverseProperty("HomeroomTeacherNameNavigation")]
    public virtual ICollection<HomeroomTeacher> HomeroomTeachers { get; set; } = new List<HomeroomTeacher>();

    [InverseProperty("User")]
    public virtual ICollection<Parent> Parents { get; set; } = new List<Parent>();

    [InverseProperty("User")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    [InverseProperty("User")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
