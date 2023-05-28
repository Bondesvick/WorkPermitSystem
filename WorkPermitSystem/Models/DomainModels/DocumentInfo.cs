using System.ComponentModel.DataAnnotations.Schema;

namespace WorkPermitSystem.Models.DomainModels
{
    public class DocumentInfo
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}