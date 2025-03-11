namespace VBEngine.SLA
{
    public class SLA
    {
        public Guid SLAId { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; } 
        public Guid ProviderId { get; set; } 
        public Guid RequesterId { get; set; } 
        public DateTime StartDate { get; set; } = DateTime.UtcNow; 
        public DateTime EndDate { get; set; } 
        public SLAStatus Status { get; set; } = SLAStatus.Active; 
        public Dictionary<string, string> Clauses { get; set; } = new(); 
    }

    public enum SLAStatus
    {
        Active,    
        Completed, 
        Breached,  
        Cancelled  
    }
}
