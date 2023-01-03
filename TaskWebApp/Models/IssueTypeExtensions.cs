namespace TaskWebApp.Models
{
    public static class IssueTypeExtensions
    {
        private static readonly Dictionary<IssueType, string> _issueTypeCssClasses = new()
        {
            [IssueType.Feature] = "badge alert-info",
            [IssueType.Task] = "badge alert-secondary",
            [IssueType.Bug] = "badge alert-danger",
        };
        public static string ToCssClass(this IssueType type)
        {
            return _issueTypeCssClasses[type];
        }
    }
}
