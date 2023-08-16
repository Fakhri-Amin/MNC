using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateProductObject : ProductObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public ProductObjectSO productObjectSO;
    }

    [SerializeField] private List<ProductObjectSO> validProductObjectSOList;
    private List<ProductObjectSO> productObjectSOList;

    private void Awake()
    {
        productObjectSOList = new List<ProductObjectSO>();
    }

    public bool TryAddIngredient(ProductObjectSO productObjectSO)
    {
        if (!validProductObjectSOList.Contains(productObjectSO)) return false;

        if (productObjectSOList.Contains(productObjectSO))
        {
            // Already has this type
            return false;
        }
        else
        {
            productObjectSOList.Add(productObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                productObjectSO = productObjectSO
            });
            return true;
        }
    }

    public List<ProductObjectSO> GetProductObjectSOList()
    {
        return productObjectSOList;
    }
}
