using System.ComponentModel.DataAnnotations;

namespace TaskWebApp.Models
{
    public class Issue
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(400)]
        public string Description { get; set; }
        public User? Assign { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Priority Priority { get; set; }
        public IssueType IssueType { get; set; }
        public IssueStatus IssueStatus { get; set; }
    }
}
