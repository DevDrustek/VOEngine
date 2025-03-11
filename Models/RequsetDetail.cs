using System.ComponentModel.DataAnnotations;

namespace VBEngine.Models
{
    public class RequsetDetail
    {
        [Key]
        public Guid RequestDetailId { get; set; }=Guid.NewGuid();
        public string ItemId { get; set; }

        public int Quantity { get; set; }
        public string Note { get; set; }  = string.Empty;
    }
}
