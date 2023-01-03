using System.Runtime.CompilerServices;

namespace TaskWebApp.Models
{
    public static class PriorityExtensions
    {
        private static readonly Dictionary<Priority, string> _priorityCssClasses = new()
        {
            [Priority.Low] = "badge btn-info",
            [Priority.Medium] = "badge btn-warning",
            [Priority.High] = "badge btn-danger",
        };
        public static string ToCssClass(this Priority priority)
        {
            return _priorityCssClasses[priority];
        }
    }
}
