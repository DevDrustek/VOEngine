using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace VBEngine.Models;

public partial class Offer
{
    public Guid OfferId { get; set; }

    public Guid ProviderId { get; set; }

    public Guid RequestId { get; set; }

    public decimal TotalPrice { get; set; }

    public List<NpgsqlRange<DateOnly>> CreatedDate { get; set; } = null!;

    public int OfferStatusId { get; set; }

    public DateTimeOffset RequestedDate { get; set; }

    public virtual OfferStatus OfferStatus { get; set; } = null!;
}
