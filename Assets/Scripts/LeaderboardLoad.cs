using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

[System.Serializable]
public class PlayerEntry
{
    public string name;
    public float time;
}

[System.Serializable]
public class LeaderboardData
{
    public List<PlayerEntry> players;
}

public class LeaderboardLoad : MonoBehaviour
{
    [Header("Assign Player1–Player10 Text Objects")]
    public TextMeshProUGUI[] playerTexts;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void getLeaderboardData();
#endif

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Debug.Log("Requesting leaderboard from JavaScript...");
        getLeaderboardData();
#else
        Debug.Log("Editor mode — WebGL leaderboard disabled.");
        DisplayEmpty();
#endif
    }

    // Called by JavaScript SendMessage
    public void OnLeaderboardData(string json)
    {
        Debug.Log("Received JSON: " + json);

        LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);

        if (data == null || data.players == null || data.players.Count == 0)
        {
            DisplayEmpty();
            return;
        }

        data.players.Sort((a, b) => a.time.CompareTo(b.time));
        DisplayLeaderboard(data.players);
    }

    void DisplayLeaderboard(List<PlayerEntry> players)
    {
        int count = Mathf.Min(players.Count, playerTexts.Length);

        for (int i = 0; i < count; i++)
        {
            string n = string.IsNullOrEmpty(players[i].name) ? "---" : players[i].name;
            playerTexts[i].text = $"{i + 1}. {n} - {players[i].time:0.00}s";
        }

        for (int i = count; i < playerTexts.Length; i++)
        {
            playerTexts[i].text = $"{i + 1}. ---";
        }
    }

    void DisplayEmpty()
    {
        for (int i = 0; i < playerTexts.Length; i++)
        {
            playerTexts[i].text = $"{i + 1}. ---";
        }
    }
}
