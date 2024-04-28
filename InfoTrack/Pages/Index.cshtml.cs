using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeoStat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoTrack.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISeoTester _seoTester;
        private readonly ISeoStatRepo _repo;

        public string SearchString { get; set; }

        public string ExpectedUrl { get; set; }

        public DateOnly CurrentDate { get; set; }

        public List<int> HitIndexes { get; set; }

        public int TestsSoFar { get => _repo.ItemCount(); }

        public IndexModel(
            ISeoTester seoTester,
            ISeoStatRepo repo)
        {
            _seoTester = seoTester;
            _repo = repo;
        }

        public void OnGet()
        {
            SearchString = Request.Query["SearchString"].FirstOrDefault() ?? "";
            ExpectedUrl = Request.Query["ExpectedUrl"].FirstOrDefault() ?? "";
            if (Request.Query["CurrentDate"].Count == 0)
            {
                CurrentDate = DateOnly.FromDateTime(DateTime.Now);
            }
            else
            {
                DateOnly.TryParse(Request.Query["CurrentDate"], out DateOnly date);
                CurrentDate = date;
            }

            HitIndexes = _seoTester.TestSearch(expectedUrl: ExpectedUrl, searchString: SearchString);
        }
    }
}
