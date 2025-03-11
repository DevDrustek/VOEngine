using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VBEngine.Models;

public partial class Provider
{
    [Key]
    public int ProviderId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string PhoneNo { get; set; } = null!;

    public virtual ICollection<QoS> Qos { get; set; } = new List<QoS>();
}
