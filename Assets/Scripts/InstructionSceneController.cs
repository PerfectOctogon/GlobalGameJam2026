using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionSceneController : MonoBehaviour
{
    [Header("Exit Button Reference")]
    public Button exitButton;

    [Header("Scene Name To Load")]
    public string menuSceneName = "MenuScene";

    void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(BackToMenu);
        }
        else
        {
            Debug.LogError("Exit Button is not assigned in Inspector!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void BackToMenu()
    {
        Debug.Log("Returning to Menu Scene...");
        SceneManager.LoadScene(menuSceneName);
    }
}
