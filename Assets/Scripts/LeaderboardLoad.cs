using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

public class LeaderboardLoad : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent;
    public GameObject entryPrefab;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void GetLeaderboardData();
#endif

    void Start()
    {
        LoadLeaderboard();
    }

    public void LoadLeaderboard()
    {
        Debug.Log("[Unity] Requesting leaderboard...");

#if UNITY_WEBGL && !UNITY_EDITOR
        GetLeaderboardData();
#else
        Debug.LogWarning("Leaderboard only works in WebGL build.");
#endif
    }

    public void OnLeaderboardData(string json)
    {
        Debug.Log("[Unity] Leaderboard received!");
        Debug.Log(json);

        // Clear old rows
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        LeaderboardWrapper wrapper =
            JsonUtility.FromJson<LeaderboardWrapper>(json);

        if (wrapper == null || wrapper.players == null)
        {
            Debug.LogError("Invalid leaderboard JSON.");
            return;
        }

        for (int i = 0; i < wrapper.players.Count; i++)
        {
            PlayerData p = wrapper.players[i];

            GameObject row = Instantiate(entryPrefab, contentParent);

            TMP_Text text = row.GetComponentInChildren<TMP_Text>();

            text.text = $"{i + 1}. {p.name} - {p.time:0.00}s";
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(
            contentParent.GetComponent<RectTransform>()
        );
    }

    [Serializable]
    public class PlayerData
    {
        public string name;
        public float time;
    }

    [Serializable]
    public class LeaderboardWrapper
    {
        public List<PlayerData> players;
    }
}
