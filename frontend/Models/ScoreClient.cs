using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace frontend
{
   public class ScoreClient
   {
      private readonly JsonSerializerOptions options = new JsonSerializerOptions()
      {
         PropertyNameCaseInsensitive = true,
         PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };

      private readonly HttpClient client;
      private readonly ILogger<ScoreClient> _logger;

      public ScoreClient(HttpClient client, ILogger<ScoreClient> logger)
      {
         this.client = client;
         this._logger = logger;
      }

      public async Task DeleteUserScores(string userName)
      {
         try
         {
            var url = $"/ScoreContoller/DeleteAllScores/{userName}";
            var responseMessage = 
               await this.client.PostAsync(url, new StringContent(userName));
            
            //Error/Ok handler logic etc
            if (responseMessage.IsSuccessStatusCode)
            {
               var responseContent = await responseMessage.Content.ReadAsStringAsync();
            }
         }
         catch(HttpRequestException ex)
         {
            _logger.LogError(ex.Message);
            throw;
         }
      }
      
      public async Task<ScoreInfo[]> GetScoresAsync(string userName)
      {
         var uri = string.IsNullOrEmpty(userName) ? "/ScoreContoller" : $"/ScoreContoller?userName={userName}";
         try {
            var responseMessage = await this.client.GetAsync(uri);
            
            //Error/Ok handler logic etc
            if(responseMessage!=null)
            {
               var stream = await responseMessage.Content.ReadAsStreamAsync();
               return await JsonSerializer.DeserializeAsync<ScoreInfo[]>(stream, options);
            }
         }
         catch(HttpRequestException ex)
         {
            _logger.LogError(ex.Message);
            throw;
         }
         
         return new ScoreInfo[] {};
      }

      public async Task AddNewScore(string user, string score)
      {
         try
         {
            var url = $"/ScoreContoller/AddScore/{user},{score}";
            var responseMessage = 
               await this.client.PostAsync(url, new MultipartContent(user, score));
            
            
            //Error/Ok handler logic etc
            if (responseMessage.IsSuccessStatusCode)
            {
               var responseContent = await responseMessage.Content.ReadAsStringAsync();
            }
         }
         catch(HttpRequestException ex)
         {
            _logger.LogError(ex.Message);
            throw;
         }
      }
      
      public void AddNewScoreAsync()
      {
      
      }
   }
}