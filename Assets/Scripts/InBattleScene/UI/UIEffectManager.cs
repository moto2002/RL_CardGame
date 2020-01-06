﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 通用UI效果管理类
/// </summary>
public class UIEffectManager : PersistentSingleton<UIEffectManager>
{
    private float offsetY = 1500;
    public Sprite[] heroStatusSps;

    /// <summary>
    /// 窗口淡入效果
    /// </summary>
    /// <param name="trans"></param>
    public void showAnimFadeIn(Transform trans)
    {
        Debug.Log("ShowAnimFadeIn:" + trans.name
            + ":(x,y):" + trans.localScale.x + "," + trans.localScale.y);
        Vector3 originalScale = trans.localScale;
        if (originalScale.x < 0.2f)
        {
            originalScale = Vector3.one;
        }
        trans.gameObject.SetActive(true);
        trans.localScale = Vector3.zero;
        trans.DOScale(originalScale.x, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        //trans.DOScaleY(1,0.2f).SetEase(Ease.OutBack).SetDelay(0.04f);
        CanvasGroup canvasGroup = trans.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        trans.gameObject.SetActive(true);
        canvasGroup.DOFade(1, 0.2f);
#if UNITY_ANDROID && ANDROID_GP
        RTAndroidBackController.Instance.addToStack(trans);
#endif
    }

    /// <summary>
    /// 窗口淡出效果
    /// </summary>
    /// <param name="trans"></param>
    public void hideAnimFadeOut(Transform trans)
    {
        Debug.Log("HideAnimFadeOut:" + trans.name 
            + ":(x,y)" + ":" + trans.localScale.x + "," + trans.localScale.y);
        Vector3 originalScale = trans.localScale;
        CanvasGroup canvasGroup = trans.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, 0.2f);
        StartCoroutine(IEHideWithDeay(trans, originalScale));
        //		trans.localScale = new Vector3(0,0,1);
        trans.DOScale(0, 0.3f).SetEase(Ease.InBack).SetUpdate(true);
#if UNITY_ANDROID && ANDROID_GP
        RTAndroidBackController.Instance.removeFromStack();
#endif
    }
    public IEnumerator IEHideWithDeay(Transform t, Vector3 vec)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        t.gameObject.SetActive(false);
        t.localScale = vec;
    }
}