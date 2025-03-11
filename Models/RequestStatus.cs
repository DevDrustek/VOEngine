using System;
using System.Collections.Generic;

namespace VBEngine.Models;

public partial class RequestStatus
{
    public int RequsetStatusId { get; set; }

    public string RequestStatusName { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
