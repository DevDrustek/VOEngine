using System;
using System.Collections.Generic;

namespace VBEngine.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid RequestId { get; set; }

    public Guid ProviderId { get; set; }

    public Guid? OfferId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTimeOffset? RequestedDate { get; set; }

    public DateTimeOffset? DeliveredDate { get; set; }

    public int? OrderStatus { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual OrderStatus? OrderStatusNavigation { get; set; }
}
