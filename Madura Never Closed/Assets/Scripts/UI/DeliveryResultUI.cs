using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";
    [SerializeField] private DeliveryManagerInCounter deliveryManagerInCounter;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        deliveryManagerInCounter.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        deliveryManagerInCounter.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        Hide();
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        Show();
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
        animator.SetTrigger(POPUP);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        Show();
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED";
        animator.SetTrigger(POPUP);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
