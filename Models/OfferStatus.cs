using System;
using System.Collections.Generic;

namespace VBEngine.Models;

public partial class OfferStatus
{
    public int OfferStatusId { get; set; }

    public string OfferStatus1 { get; set; } = null!;

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
