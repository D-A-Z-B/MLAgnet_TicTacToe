using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;
    [SerializeField] private TextMeshProUGUI winnerText;

    private void Awake() {
        Close();
    }

    public void SetWinnerText(string winnerName)
    {
        if (winnerText != null)
        {
            if (winnerName == "Draw")
            {
                winnerText.text = "Draw";
            }
            else
            {
                winnerText.text = $"Winner: {winnerName}";
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Open()
    {
        rootPanel.SetActive(true);
    }
    
    public void Close()
    {
        rootPanel.SetActive(false);
    }
}
