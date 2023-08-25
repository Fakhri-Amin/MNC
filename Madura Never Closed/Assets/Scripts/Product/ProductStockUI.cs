using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProductStockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStock;
    public int currentStock;
    [SerializeField] private int maxStock;

    void Start()
    {
        currentStock = maxStock;
        UpdateUIStock();
    }

    private void UpdateUIStock()
    {
        textStock.text = currentStock + "/" + maxStock;
    }

    public void ReduceStock()
    {
        currentStock--;
        UpdateUIStock();
    }
}
