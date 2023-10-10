using backend.Communication.Responses;
using backend.Communication.Services;
using backend.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Unity;

namespace backend.Controllers;

[ApiController]
[Route("/[controller]")]
public class ScoreContoller : Controller
{
    private readonly IScoreService _scoreService;
    public ScoreContoller(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }
    
    /// <summary>
    /// Get all score list
    /// with optional parameter - sort data using Like query by user name
    /// </summary>
    /// <param name="userName">Optional user name filter</param>
    [HttpGet, Route("")]
    public IEnumerable<UserScoreDto> Get(string userName = null)
    { 
        var data = _scoreService.GetScores(userName);
        return data.Select(s => UserScoreDto.Convert(s)).ToArray();
    }
    
    [HttpPost, Route("AddScore/{userName},{score}")]
    public BaseResponse AddScore(string userName, int score)
    {
        _scoreService.AddScore(userName, score);
        return BaseResponse.Ok;
    }
    
    [HttpPost, Route("DeleteAllScores/{userName}")]
    public BaseResponse DeleteAllScores(string userName)
    {
        _scoreService.DeleteAllScores(userName);
        return BaseResponse.Ok;
    }
}