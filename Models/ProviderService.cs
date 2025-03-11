using System;
using System.Collections.Generic;

namespace VBEngine.Models;

public partial class ProviderService
{
    public int ProviderId { get; set; }

    public int ServiceId { get; set; }

    public DateOnly? AddedDate { get; set; }
}
