using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using TaskWebApp.Models;

namespace TaskWebApp.Pages.Task
{
    public class CreateIssueModel : PageModel
    {
        [BindProperty]
        public Issue Issue { get; set; }
        public ILogger<CreateIssueModel> _logger { get; }

        public CreateIssueModel(ILogger<CreateIssueModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost(Issue issue)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            issue.CreatedDate = DateTime.Now;
            issue.Id= Guid.NewGuid();

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(issue), Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await client.PostAsync("https://localhost:7115/api/Issue", content);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Issue = JsonConvert.DeserializeObject<Issue>(apiResponse);
                }
            }

            return RedirectToPage("/Index");
        }
    }
}
