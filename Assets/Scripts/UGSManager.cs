using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using System.Threading.Tasks;
using Unity.Services.Leaderboards;
using Newtonsoft.Json;
using NUnit.Framework;
using Unity.Services.Leaderboards.Models;
using System.Collections.Generic;

public class UGSManager : MonoBehaviour
{
    public static UGSManager Instance;

    async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            Debug.Log("Production Environment Loading...");
            var options = new InitializationOptions();
            options.SetEnvironmentName("production");
            await UnityServices.InitializeAsync(options);
            Debug.Log("Production Environment Loaded");
            await SignUpAnonymousAsync();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    async Task SignUpAnonymousAsync()
    {
        // Clearing session allows multiple scores submitted on the same computer (after a restart)
        // Delete/Comment out line below if not needed
        AuthenticationService.Instance.ClearSessionToken();

        // Create a profile for the player to quickly start playing.
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log("Sign in as guest!");

        string randomName = "Guest" + Random.Range(1, 9999);
        await AuthenticationService.Instance.UpdatePlayerNameAsync(randomName);
        Debug.Log($"Player Name: {AuthenticationService.Instance.PlayerName}");

    }

    public async void AddScore(string leaderboardID, int score)
    {
        Debug.Log("Adding score to UGS Leaderboard...");
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, score);
        Debug.Log("Score submitted");
    }

    public async void GetScores(string leaderboardID)
    {
        Debug.Log("Loeading player scores...");
        var scoreResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));

        List<LeaderboardEntry> entries = scoreResponse.Results;

        foreach(var entry in entries)
        {
            Debug.Log($"Name: {entry.PlayerName} - {entry.Score}");
        }

        GameManager.Instance.ShowLeaderboardUI(entries);
    }
}
