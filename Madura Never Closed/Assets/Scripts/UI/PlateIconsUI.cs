using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateProductObject plateProductObject;
    [SerializeField] private Transform iconTemplate;

    private void Start()
    {
        iconTemplate.gameObject.SetActive(false);
        plateProductObject.OnIngredientAdded += PlateProductObject_OnIngredientAdded;
    }

    private void PlateProductObject_OnIngredientAdded(object sender, PlateProductObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (ProductObjectSO productObjectSO in plateProductObject.GetProductObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(productObjectSO);
        }
    }
}
