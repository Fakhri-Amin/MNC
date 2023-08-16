using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProductObjectParent
{
    public Transform GetProductObjectFollowTransform();

    public void SetProductObject(ProductObject productObject);

    public ProductObject GetProductObject();

    public void ClearProductObject();

    public bool HasProductObject();
}
