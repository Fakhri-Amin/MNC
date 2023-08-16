using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductObject : MonoBehaviour
{
    [SerializeField] private ProductObjectSO productObjectSO;

    private IProductObjectParent productObjectParent;

    public ProductObjectSO GetProductObjectSO()
    {
        return productObjectSO;
    }

    public void SetProductObjectParent(IProductObjectParent productObjectParent)
    {
        if (this.productObjectParent != null)
        {
            this.productObjectParent.ClearProductObject();
        }

        this.productObjectParent = productObjectParent;

        if (productObjectParent.HasProductObject())
        {
            Debug.LogError("IProductObjectParent already has a Kitchen Object!");
        }

        productObjectParent.SetProductObject(this);

        transform.parent = productObjectParent.GetProductObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IProductObjectParent GetProductObjectParent()
    {
        return productObjectParent;
    }

    public void DestroySelf()
    {
        productObjectParent.ClearProductObject();
        Destroy(gameObject);
    }

    public static ProductObject SpawnProductObject(ProductObjectSO productObjectSO, IProductObjectParent productObjectParent)
    {
        Transform productObjectTransform = Instantiate(productObjectSO.prefab);

        ProductObject productObject = productObjectTransform.GetComponent<ProductObject>();

        productObject.SetProductObjectParent(productObjectParent);

        return productObject;
    }

    public bool TryGetPlate(out PlateProductObject plateProductObject)
    {
        if (this is PlateProductObject)
        {
            plateProductObject = this as PlateProductObject;
            return true;
        }
        else
        {
            plateProductObject = null;
            return false;
        }
    }
}
