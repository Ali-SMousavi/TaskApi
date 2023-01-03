using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using TaskWebApp.Models;
using TaskWebApp.Pages.Account;

namespace TaskWebApp.Pages.Task
{
    public class EditIssueModel : PageModel
    {
        public ILogger<EditIssueModel> _logger { get; }
        public Issue Issue { get; set; }
        public IEnumerable<User> Users = Enumerable.Empty<User>();
        public SelectList UserSL { get; set; }
        public EditIssueModel(ILogger<EditIssueModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            using (HttpClient client = new HttpClient())
            {

                using HttpResponseMessage response = await client.GetAsync("https://localhost:7115/api/Issue/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Issue = JsonConvert.DeserializeObject<Issue>(apiResponse);
                }

                using HttpResponseMessage response_users = await client.GetAsync("https://localhost:7115/api/User");

                if (response_users.IsSuccessStatusCode)
                {
                    string apiResponse_users = await response_users.Content.ReadAsStringAsync();
                    Users = JsonConvert.DeserializeObject<IEnumerable<User>>(apiResponse_users);

                    UserSL = new SelectList(Users, "Id", "FirstName" );
                }

            }
            return Page();
        }
        public async Task<IActionResult> OnPost(Issue Issue)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Issue), Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await client.PutAsync("https://localhost:7115/api/Issue/" + Issue.Id, content);

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
