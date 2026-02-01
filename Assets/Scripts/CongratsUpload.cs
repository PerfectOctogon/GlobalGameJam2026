using UnityEngine;
using System.Runtime.InteropServices;

public class CongratsUpload : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void submitScore(string name, float time);
#endif

    private static bool uploaded = false;

    void Start()
    {
        if (uploaded) return;
        uploaded = true;

        UploadScore();
    }

    void UploadScore()
    {
        string playerName = GameSession.playerName;
        float finalTime = GameSession.Instance.currentTime;

        Debug.Log("Uploading score: " + playerName + " - " + finalTime);

#if UNITY_WEBGL && !UNITY_EDITOR
        submitScore(playerName, finalTime);
#else
        Debug.Log("Upload only works in WebGL build.");
#endif
    }
}
