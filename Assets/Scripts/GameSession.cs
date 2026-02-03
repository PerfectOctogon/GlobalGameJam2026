using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    public string playerName;
    public float currentTime;
    public bool timerRunning;

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

        Debug.Log("Timer started.");
    }

    public void EndRun()
    {
        timerRunning = false;
        Debug.Log("Timer stopped at: " + currentTime);
    }
}
