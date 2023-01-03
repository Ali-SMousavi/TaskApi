using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TaskWebApp.Models
{
    public enum IssueStatus
    {
        [Display(Name = "Not Started")] NotStarted,
        [Display(Name = "In Progress")] InProgress,
        Testing,
        Completed
    }
}