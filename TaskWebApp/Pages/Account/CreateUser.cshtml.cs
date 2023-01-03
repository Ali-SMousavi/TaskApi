using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using TaskWebApp.Models;

namespace TaskWebApp.Pages.Account
{
    public class CreateUserModel : PageModel
    {
        public ILogger<CreateUserModel> _logger { get; }
        public User User { get; set; }
        public CreateUserModel(ILogger<CreateUserModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost(User user)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            user.Id = Guid.NewGuid();

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await client.PostAsync("https://localhost:7115/api/User", content);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    User = JsonConvert.DeserializeObject<User>(apiResponse);
                }
                else
                {
                    return RedirectToPage("", new { status = "NotCreated" });
                }
            }

            return RedirectToPage("/Account/User");
        }
    }
}
