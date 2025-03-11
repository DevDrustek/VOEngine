using System;
using System.Collections.Generic;

namespace VBEngine.Models;

public partial class Request
{
    public Guid RequestId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime RequestDate { get; set; }

    public int RequsetStatus { get; set; }

    public long RequesterId { get; set; }

    public string RequsetServices { get; set; }

    public  string RequsetDetail { get; set; }   

    public virtual RequestStatus RequsetStatusNavigation { get; set; } = null!;
}


