using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenuController : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void OnPlayPressed()
    {
        string playerName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Player name required.");
            return;
        }

        if (playerName.Length > 10)
            playerName = playerName.Substring(0, 10);

        GameSession.Instance.StartRun(playerName);

        SceneManager.LoadScene("GameScene"); // your gameplay scene
    }
}