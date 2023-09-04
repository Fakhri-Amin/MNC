using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompeleted += DeliveryManager_OnRecipeCompeleted;
        UpdateVisual();
    }



    private void DeliveryManager_OnRecipeSpawned(object sender, DeliveryManager.OnRecipeSpawnedEventArgs e)
    {
        UpdateVisual(e.counterNumber);
    }

    private void DeliveryManager_OnRecipeCompeleted(object sender, DeliveryManager.OnRecipeCompeletedEventArgs e)
    {
        UpdateVisual(e.counterNumber);
    }

    private void UpdateVisual(int counterNumber = 0)
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO, counterNumber);
        }
    }
}
