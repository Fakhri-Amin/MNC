using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCustomerVisual : MonoBehaviour
{

    [SerializeField] private GameObject customerVisual;

    private Vector3 customerStartPosition;

    private void Start()
    {
        DeliveryManager.Instance.OnCustomerSpawned += DeliveryManager_OnCustomerSpawned;
        customerVisual.gameObject.SetActive(false);
        customerStartPosition = customerVisual.transform.position;
    }

    private void DeliveryManager_OnCustomerSpawned(object sender, EventArgs e)
    {
        StartCoroutine(CustomerMovingRoutine());
    }

    private IEnumerator CustomerMovingRoutine()
    {
        customerVisual.transform.position = customerVisual.transform.position + Vector3.forward * -2;
        customerVisual.gameObject.SetActive(true);

        float travelPercent = 0;
        while (travelPercent < 1)
        {
            customerVisual.transform.position = Vector3.Lerp(customerVisual.transform.position, customerStartPosition, travelPercent);
            travelPercent += Time.deltaTime;
            Debug.Log(travelPercent);
            yield return new WaitForEndOfFrame();
        }
    }
}
