using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskWebApp.Models;

namespace TaskWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<Issue> Issues = Enumerable.Empty<Issue>();
        public Issue Issue { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            using (HttpClient client = new HttpClient())
            {
                
                using HttpResponseMessage response = await client.GetAsync("https://localhost:7115/api/Issue");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Issues = JsonConvert.DeserializeObject<IEnumerable<Issue>>(apiResponse);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEditIssue(Issue issue)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(issue), Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await client.PutAsync("https://localhost:7115/api/Issue/" + issue.Id, content);

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