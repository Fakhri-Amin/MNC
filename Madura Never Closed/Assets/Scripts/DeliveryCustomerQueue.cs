using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryCustomerQueue : MonoBehaviour
{
    private Queue<GameObject> customerList = new Queue<GameObject>();
    private List<Vector3> positionList;

    private void Start()
    {
        DeliveryManager.Instance.OnCustomerSpawned += DeliveryManager_OnCustomerSpawned;
    }

    private void DeliveryManager_OnCustomerSpawned(object sender, EventArgs e)
    {

    }

    public DeliveryCustomerQueue(List<Vector3> positionList)
    {
        this.positionList = positionList;
    }

    public bool CanAddCustomer()
    {
        return customerList.Count < positionList.Count;
    }

    public void AddCustomer(GameObject customer)
    {
        customerList.Enqueue(customer);
    }
}
