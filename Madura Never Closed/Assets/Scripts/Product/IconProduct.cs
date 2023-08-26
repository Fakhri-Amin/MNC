using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconProduct : MonoBehaviour
{
    [SerializeField] private Transform iconProduct;
    [SerializeField] private Image icon;
    [SerializeField] private ProductObjectSO[] iProduct;
    // Start is called before the first frame update
    void Start()
    {
        icon.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (iconProduct.GetComponentInChildren<ProductObject>() != null)
        {
            icon.gameObject.SetActive(true);
            foreach(ProductObjectSO ico in iProduct)
            {
                if(ico.objectName == iconProduct.GetComponentInChildren<ProductObject>().GetProductObjectSO().objectName)
                {
                    icon.sprite = ico.sprite;
                }
            }
        }
        else
        {
            icon.gameObject.SetActive(false);
        }
    }
}
