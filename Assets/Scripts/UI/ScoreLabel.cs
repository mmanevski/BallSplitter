using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    private TextMeshProUGUI scoreLabel;
    void Awake()
    {
        scoreLabel = GetComponent<TextMeshProUGUI>();
        GameManager.updateScore.AddListener(OnUpdateScore);
    }

    private void OnUpdateScore(int newScore)
    {
        scoreLabel.text = newScore.ToString();
    }

    void OnDestroy()
    {
        GameManager.updateScore.RemoveListener(OnUpdateScore);
    }
}
