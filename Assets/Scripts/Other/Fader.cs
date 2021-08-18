using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    // Awake is called before any other
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //immediately turns the screen white
    public void ImmediateWhite()
    {
        canvasGroup.alpha = 1;
    }

    //fade the canvas out
    public IEnumerator FadeOut(float fadeTime)
    {
        while(canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    //fade the canvas in
    public IEnumerator FadeIn(float fadeTime)
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
    }

}
