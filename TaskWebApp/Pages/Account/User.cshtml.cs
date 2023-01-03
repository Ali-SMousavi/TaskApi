using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TaskWebApp.Models;

namespace TaskWebApp.Pages.Account
{
    public class UserModel : PageModel
    {
        private readonly ILogger<UserModel> _logger;
        public IEnumerable<User> Users = Enumerable.Empty<User>();

        public UserModel(ILogger<UserModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            using (HttpClient client = new HttpClient())
            {

                using HttpResponseMessage response = await client.GetAsync("https://localhost:7115/api/User");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Users = JsonConvert.DeserializeObject<IEnumerable<User>>(apiResponse);
                }
            }
            return Page();
        }
    }
}