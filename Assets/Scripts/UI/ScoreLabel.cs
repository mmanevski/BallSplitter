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
        BumpText();
    }

    void OnDestroy()
    {
        GameManager.updateScore.RemoveListener(OnUpdateScore);
    }
    
    private void BumpText()
    {
        if (gameObject.LeanIsTweening())
            return;
        
        float _scaleAdd = 0.1f;
        float _bumpTime = 0.1f;
        Vector3 _startScale = scoreLabel.transform.localScale;
        Vector3 _scaleTo = new Vector3(_startScale.x +_scaleAdd, _startScale.x +_scaleAdd, 1f);
        LeanTween.scale(scoreLabel.rectTransform, _scaleTo, _bumpTime).setEaseInBounce().setOnComplete(
            () => { LeanTween.scale(scoreLabel.rectTransform, _startScale, _bumpTime);});
    }
}
