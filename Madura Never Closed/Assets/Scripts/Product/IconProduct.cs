using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconProduct : MonoBehaviour
{
    [SerializeField] private Transform iconProduct;
    [SerializeField] private Image icon;
    // Start is called before the first frame update
    void Start()
    {
        icon.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(iconProduct.GetComponentInChildren<ProductObject>() != null)
        {
            icon.gameObject.SetActive(true);
        }
    }
}
