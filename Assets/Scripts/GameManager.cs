using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playInfoText;
    public Button RestartButton;
    private int score = 0;
    private static int bestScore = 0;

    void Start()
    {
        RestartButton.onClick.AddListener(Restart);
        RestartButton.gameObject.SetActive(false);
    }
    void Update()
    {
        playInfoText.text = $"Score: {score}\n"+
                            $"Best: {bestScore}";
    }

    public void AddScore(int value = 1)
    {
        score += value;

        if (score > bestScore)
            bestScore = score;
        Debug.Log(score);
    }

    public void GameOver()
    {
        RestartButton.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
