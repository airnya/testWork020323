using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private ScoreClient _scoreClient;

        public IndexModel(ILogger<IndexModel> logger, ScoreClient scoreClient)
        {
            _logger = logger;
            _scoreClient = scoreClient;
        }

        public ScoreInfo[] ScoreInfoList { get; set; } = Array.Empty<ScoreInfo>();
        
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        
        [BindProperty]
        public string Name { get; set; }
        
        [BindProperty]
        public string NewName { get; set; }
        [BindProperty]
        public string NewScore { get; set; }

        public async Task OnPostAdd()
        {
            try
            {
                await _scoreClient.AddNewScore(NewName, NewScore);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            
            await Refresh();
        }
        
        public async Task OnPostDelete()
        {
            try
            {
                await _scoreClient.DeleteUserScores(Name);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            
            await Refresh();
        }

        public async Task Refresh()
        {
            try
            {
                ScoreInfoList = await _scoreClient.GetScoresAsync(SearchString);
                if(ScoreInfoList?.Count()==0)
                    ErrorMessage="We dont have any highscore. Try again tomorrow.";
                else
                    ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            return null;
        }
        
        public string ErrorMessage {get;set;}

        public async Task OnGet([FromServices]ScoreClient client)
        {
            await Refresh();
        }
        
        public async Task AddNew([FromServices]ScoreClient client)
        {

        }
    }
}
