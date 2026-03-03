using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEffect : MonoBehaviour
{
    public Text scoreText;
    public RectTransform rectTransform;
    public Animator animator;
    private Coroutine coroutine;
    public void SetScoreEffect(int addScore,Vector3 postion)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            animator.StopPlayback();
        }
        animator.Play("ScoreEffectAnimation", -1);
        scoreText.text = "+" + addScore;
        rectTransform.position = postion;
        gameObject.SetActive(true);
        
        coroutine=StartCoroutine(GameManager.Instance.ActionCallAfterTime(1f, true, () =>
        {
            gameObject.SetActive(false);
        }));
    }
    
}
