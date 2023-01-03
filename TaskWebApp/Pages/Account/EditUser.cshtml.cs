using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using TaskWebApp.Models;

namespace TaskWebApp.Pages.Account
{
    public class EditUserModel : PageModel
    {
        public ILogger<EditUserModel> _logger { get; }
        public User User { get; set; }
        public EditUserModel(ILogger<EditUserModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            using (HttpClient client = new HttpClient())
            {

                using HttpResponseMessage response = await client.GetAsync("https://localhost:7115/api/User/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    User = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(User user)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await client.PutAsync("https://localhost:7115/api/User/" + user.Id, content);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    User = JsonConvert.DeserializeObject<User>(apiResponse);
                }
                else
                {
                    return RedirectToPage("", new { status = "NotUpdated" });
                }
            }

            return RedirectToPage("/Account/User");
        }
    }
}
