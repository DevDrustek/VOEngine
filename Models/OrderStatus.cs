﻿using System;
using System.Collections.Generic;

namespace VBEngine.Models;

public partial class OrderStatus
{
    public int OrderStatusId { get; set; }

    public string OrderStatusName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
