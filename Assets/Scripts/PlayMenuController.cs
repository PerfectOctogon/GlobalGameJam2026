using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenuController : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInputField;
    public Button playButton;
    public TextMeshProUGUI warningText;

    [Header("Scene Settings")]
    public string gameSceneName = "AdiRooms";

    void Start()
    {
        if (warningText != null)
            warningText.text = "";

        if (playButton != null)
            playButton.onClick.AddListener(OnPlayPressed);
    }

    public void OnPlayPressed()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            ShowWarning("Please enter a name!");
            return;
        }

        if (playerName.Length > 10)
        {
            ShowWarning("Name must be 10 characters or less.");
            return;
        }

        if (GameSession.Instance == null)
        {
            Debug.LogError("GameSession object not found in scene!");
            return;
        }

        GameSession.Instance.StartRun(playerName);

        Debug.Log("Run started for: " + playerName);

        SceneManager.LoadScene(gameSceneName);
    }

    void ShowWarning(string msg)
    {
        if (warningText != null)
            warningText.text = msg;

        Debug.LogWarning(msg);
    }
}