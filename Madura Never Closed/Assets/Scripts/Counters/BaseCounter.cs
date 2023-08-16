using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IProductObjectParent
{
    public static event EventHandler OnAnyPlacedHere;
    public static void ResetStaticData()
    {
        OnAnyPlacedHere = null;
    }

    [SerializeField] private Transform counterTopPoint;

    private ProductObject productObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player)
    {
        // Debug.LogError("BaseCounter.InteractAlternate()");
    }

    public Transform GetProductObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetProductObject(ProductObject productObject)
    {
        this.productObject = productObject;
        if (productObject != null) OnAnyPlacedHere?.Invoke(this, EventArgs.Empty);
    }

    public ProductObject GetProductObject()
    {
        return productObject;
    }

    public void ClearProductObject()
    {
        productObject = null;
    }

    public bool HasProductObject()
    {
        return productObject != null;
    }
}
