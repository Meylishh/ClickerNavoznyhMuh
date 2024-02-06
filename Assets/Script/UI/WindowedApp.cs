using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WindowedApp : MonoBehaviour
{
    [Header("Open/Close Window")]
    [SerializeField] private Button closeButton;
    [SerializeField] private float scaleOnOpen;
    [SerializeField] private float scaleOnClose;
    [SerializeField] private float openDuration;
    [SerializeField] private bool closedOnStart;

    private void Start()
    {
        if (closedOnStart)
        {
            gameObject.SetActive(false);
        }
    }

    private async UniTask CloseAppAsync()
    {
        closeButton.interactable = false;
        
        await transform.DOScale(scaleOnClose, openDuration).ToUniTask();

        closeButton.interactable = true;
        gameObject.SetActive(false);
    }

    private async UniTask OpenAppAsync()
    {
        gameObject.SetActive(true);
        closeButton.interactable = false;
        
        await transform.DOScale(scaleOnOpen, openDuration).ToUniTask();

        closeButton.interactable = true;
    }

    public void CloseApp()
    {
        CloseAppAsync().Forget();
    }

    public void OpenApp()
    {
        OpenAppAsync().Forget();
    }
}