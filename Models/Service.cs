using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VBEngine.Models;

public partial class Service
{
    [Key]
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string Description { get; set; } = null!;
}
