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
        // Load values from GameSession
        string playerName = GameSession.playerName;
        float finalTime = GameSession.Instance.currentTime;

        if (playerNameText != null)
            playerNameText.text = playerName + "!";

        if (playerTimeText != null)
            playerTimeText.text = $"{finalTime:0.00} seconds";

        Debug.Log("Congrats Screen Loaded:");
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
