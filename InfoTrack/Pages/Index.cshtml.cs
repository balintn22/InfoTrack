using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfoTrack.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string SearchString { get; set; }

        public DateOnly CurrentDate { get; set; }

        public List<int> HitIndexes { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            SearchString = Request.Query["SearchString"].FirstOrDefault() as string ?? "";

            int maxNumberOfHits = 10;
            int hitCount = Random.Shared.Next(maxNumberOfHits);
            HitIndexes = new();
            for (int i = 0; i < hitCount; i++)
                HitIndexes.Add(Random.Shared.Next(100));
        }
    }
}
