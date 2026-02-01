using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    public static string playerName;
    public static float currentTime;
    public bool timerRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (timerRunning)
            currentTime += Time.deltaTime;
    }

    public void StartRun(string name)
    {
        playerName = name;
        currentTime = 0f;
        timerRunning = true;
    }

    public void EndRun()
    {
        timerRunning = false;
    }
}
