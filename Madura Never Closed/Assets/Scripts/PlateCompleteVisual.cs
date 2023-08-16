using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct ProductObjectSO_GameObject
    {
        public ProductObjectSO productObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateProductObject plateProductObject;
    [SerializeField] private List<ProductObjectSO_GameObject> productObjectSOGameObjectList;

    private void Start()
    {
        plateProductObject.OnIngredientAdded += PlateProductObject_OnIngredientAdded;

        foreach (ProductObjectSO_GameObject productObjectSOGameObject in productObjectSOGameObjectList)
        {
            productObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateProductObject_OnIngredientAdded(object sender, PlateProductObject.OnIngredientAddedEventArgs e)
    {
        foreach (ProductObjectSO_GameObject productObjectSOGameObject in productObjectSOGameObjectList)
        {
            if (productObjectSOGameObject.productObjectSO == e.productObjectSO)
            {
                productObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
