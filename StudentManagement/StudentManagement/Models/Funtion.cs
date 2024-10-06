using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class Funtion
{
    [Key]
    public int FunctionId { get; set; }

    [StringLength(50)]
    public string? FunctionName { get; set; }

    [StringLength(50)]
    public string? Route { get; set; }

    public int? RoleId { get; set; }

    [StringLength(50)]
    public string? Icon { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Funtions")]
    public virtual Role? Role { get; set; }
}
