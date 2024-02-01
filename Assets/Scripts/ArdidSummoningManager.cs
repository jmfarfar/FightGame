using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdidSummoningManager : MonoBehaviour
{
    [SerializeField] private GameObject handSummoning;
    [SerializeField] private float dissolveTime;
    private Animator animator;
    private Material handSummoningMaterial;
    private bool isSummoningActive;

    void Awake()
    {
        animator = GetComponent<PlayerStateMachine>().Animator;
        handSummoningMaterial = 
            handSummoning.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial;
    }

    void Update()
    {
        if(animator == null) return;

        if (animator.GetFloat("FreeLookSpeed") > 0.5f && !handSummoning.activeSelf)
        {
            isSummoningActive = true;

            StopAllCoroutines();

            StartCoroutine(SummoningAppearEffect(handSummoningMaterial));
        }
        else if (animator.GetFloat("FreeLookSpeed") < 0.5f && isSummoningActive)
        {
            isSummoningActive = false;

            StopAllCoroutines();

            StartCoroutine(SummoningDissolveEffect(handSummoningMaterial));
        }
    }

    private IEnumerator SummoningAppearEffect(Material targetMaterial)
    {
        float timer = dissolveTime;

        handSummoning.SetActive(true);

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            targetMaterial.SetFloat("_Dissolve", Mathf.Lerp(
                targetMaterial.GetFloat("_Dissolve"),
                0f,
                Time.deltaTime / dissolveTime
            ));
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator SummoningDissolveEffect(Material targetMaterial)
    {
        float timer = dissolveTime;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            targetMaterial.SetFloat("_Dissolve", Mathf.Lerp(
                targetMaterial.GetFloat("_Dissolve"),
                1f,
                Time.deltaTime / dissolveTime
            ));
            yield return new WaitForEndOfFrame();
        }

        handSummoning.SetActive(false);
    }
}
