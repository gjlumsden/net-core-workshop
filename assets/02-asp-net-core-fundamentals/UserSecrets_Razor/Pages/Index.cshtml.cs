using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace UserSecrets.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TheBigSecret = null;

        public void OnGet()
        {
            TheBigSecret = _configuration["TheBigSecret"];
        }
    }
}
