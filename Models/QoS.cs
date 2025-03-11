using System;
using System.Collections.Generic;

namespace VBEngine.Models;

public partial class QoS
{
    public int QoSid { get; set; }

    public int ProviderId { get; set; }

    public int Rank { get; set; }

    public decimal ResponseTime { get; set; }

    public decimal Reliability { get; set; }

    public decimal Availability { get; set; }

    public virtual Provider Provider { get; set; } = null!;
}
