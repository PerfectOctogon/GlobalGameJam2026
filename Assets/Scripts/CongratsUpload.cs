using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class CongratsUploader : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerTimeText;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void submitScore(string name, float time);
#endif

    private static bool uploaded = false;

    void Start()
    {
        if (GameSession.Instance == null)
        {
            Debug.LogError("GameSession not found!");
            return;
        }

        GameSession.Instance.EndRun();

        string playerName = GameSession.Instance.playerName;
        float finalTime = GameSession.Instance.currentTime;

        if (playerNameText != null)
            playerNameText.text = "Congrats " + playerName + "!";

        if (playerTimeText != null)
            playerTimeText.text = $"{finalTime:0.00} seconds";

        Debug.Log("Final Score:");
        Debug.Log("Player = " + playerName);
        Debug.Log("Time = " + finalTime);

        if (uploaded) return;
        uploaded = true;

#if UNITY_WEBGL && !UNITY_EDITOR
        submitScore(playerName, finalTime);
        Debug.Log("Score upload triggered.");
#else
        Debug.Log("Upload works only in WebGL build.");
#endif
    }
}
