using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void SetKitchenObjectSO(ProductObjectSO productObjectSO)
    {
        icon.sprite = productObjectSO.sprite;
    }
}
