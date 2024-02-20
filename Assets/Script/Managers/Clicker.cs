using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Clicker : MonoBehaviour
{
    [Header("Clicker")] 
    [SerializeField] private GameObject errorMessagePrefab;
    [SerializeField] private GameObject spawnParent;
    [SerializeField] private float errorWindowAnimationScale;
    

    [Header("Buffs shop")] 
    [SerializeField] private GameObject shopBuffsPanel;

    [SerializeField] private Button showBuffsPanelButton;
    [SerializeField] private Button hideBuffsPanelButton;
    [SerializeField] private float xMoveShow;
    [SerializeField] private float xMoveHide;
    private bool buffsPanelOpen;

    private void Start()
    {
        buffsPanelOpen = false;
        shopBuffsPanel.SetActive(false);


    }

    private async UniTask ShowPanelAsync()
    {
        Events.ClicksUpdated?.Invoke();
        
        if (!buffsPanelOpen)
        {
            shopBuffsPanel.SetActive(true);
            showBuffsPanelButton.interactable = false;

            await shopBuffsPanel.transform.DOLocalMoveX(xMoveShow, 0.5f).ToUniTask();

            showBuffsPanelButton.interactable = true;
            buffsPanelOpen = true;
        }
    }
    
    private async UniTask HidePanelAsync()
    {
        hideBuffsPanelButton.interactable = false;
        showBuffsPanelButton.interactable = false;

        await shopBuffsPanel.transform.DOLocalMoveX(xMoveHide, 0.5f).ToUniTask();

        hideBuffsPanelButton.interactable = true;
        showBuffsPanelButton.interactable = true;

        buffsPanelOpen = false;
        shopBuffsPanel.SetActive(false);
    }

    public void ShowPanel()
    {
        ShowPanelAsync().Forget();
    }

    public void HidePanel()
    {
        HidePanelAsync().Forget();
    }

    public void OnClickerCloseButtonPress()
    {
        var errorWindow = Instantiate(errorMessagePrefab, spawnParent.transform);
        
        var rectTransform = errorWindow.GetComponent<RectTransform>();
        var position = rectTransform.anchoredPosition;
        
        position.x += Random.Range(-100, 100);
        position.y += Random.Range(-50, 50);
        
        rectTransform.anchoredPosition = position;
        errorWindow.transform.DOScale(errorWindowAnimationScale, 0.1f).SetLoops(2, LoopType.Yoyo);
    }
    
}
