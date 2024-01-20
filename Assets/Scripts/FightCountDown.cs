using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FightCountDown : MonoBehaviour
{
    [Header("Start conditions")]
    [SerializeField] private float startDelay;

    [Header("Objects")]
    [SerializeField] private Transform parentObject;
    private Image[] countDownPanels;

    [Header("Animation")]
    [SerializeField] private AnimationCurve animationCurve;

    [Header("End conditions")]
    [SerializeField] private UnityEvent onCountDownFinish;
    int counter = 1;

    [Header("Sounds")]
    [SerializeField] private AudioClip clip;

    IEnumerator Start()
    {
        countDownPanels = parentObject.GetComponentsInChildren<Image>(true);

        yield return new WaitForSeconds(startDelay);

        StartCoroutine(FadeInSprite(countDownPanels[counter]));

        yield return new WaitUntil(() => counter == countDownPanels.Length - 1);

        onCountDownFinish?.Invoke();
    }

    private IEnumerator FadeInSprite(Image image)
    {
        float timer = 0f;
        image.gameObject.SetActive(true);
        while (timer < 1f)
        {
            image.rectTransform.localScale = new Vector2(animationCurve.Evaluate(timer), animationCurve.Evaluate(timer));
            image.color = new Color(1f, 1f, 1f, animationCurve.Evaluate(1 - timer));
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        AudioManager.Instance.PlaySound(clip);
        image.gameObject.SetActive(false);
        counter++;
        if (counter < countDownPanels.Length)
        {
            StartCoroutine(FadeInSprite(countDownPanels[counter]));
        }
    }
}
