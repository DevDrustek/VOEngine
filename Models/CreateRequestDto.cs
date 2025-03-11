namespace VBEngine.Models
{
    public class CreateRequestDto
    {
        public long RequesterId { get; set; }
        public string RequestServices { get; set; }
        public string RequestDetail { get; set; }
        public DateTime? RequestedDate { get; set; }
    }

}
