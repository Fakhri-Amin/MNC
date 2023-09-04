using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryManagerInCounter : MonoBehaviour
{
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    [SerializeField] private GameObject customerVisual;
    [SerializeField] private TMP_Text counterNumberText;

    private List<RecipeSO> waitingRecipeSOList;
    private Vector3 customerStartPosition;
    private GameObject customer;
    private int counterNumber;


    // Start is called before the first frame update
    void Start()
    {
        customerStartPosition = customerVisual.transform.position;
        customerVisual.SetActive(false);
        counterNumberText.text = counterNumber.ToString();
    }

    public void DeliverRecipe(PlateProductObject plateProductObject)
    {
        waitingRecipeSOList = DeliveryManager.Instance.GetWaitingRecipeSOList();

        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.productObjectSOList.Count == plateProductObject.GetProductObjectSOList().Count)
            {
                // Has the same number of ingredients
                bool plateContentMatchesRecipe = true;

                foreach (ProductObjectSO recipeProductObjectSO in waitingRecipeSO.productObjectSOList)
                {
                    // Cycling through all the ingredient in Recipe
                    bool ingredientFound = false;
                    foreach (ProductObjectSO plateProductObjectSO in plateProductObject.GetProductObjectSOList())
                    {
                        // Cycling through all the ingredient in Plate
                        if (plateProductObjectSO == recipeProductObjectSO)
                        {
                            // Ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // This Recipe ingredient was not found on the Plate
                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    // Player delivered the correct Recipe!
                    DeliveryManager.Instance.RemoveRecipeSOFromList(i);
                    DeliveryManager.Instance.FireOnDeliveryManagerCompletedEvent();
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    RemoveCustomer();
                    return;
                }
            }
        }

        // No mathces found
        // Player did not deliverd the Recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        RemoveCustomer();
    }

    public void SetCounterNumber(int counterNumber)
    {
        this.counterNumber = counterNumber;
    }

    public int GetCounterNumber()
    {
        return counterNumber;
    }

    public void SpawnCustomer()
    {
        StartCoroutine(CustomerMoveToCounter());
    }

    public void RemoveCustomer()
    {
        customer = null;
        customerVisual.SetActive(false);
    }

    public bool IsCustomerEmpty()
    {
        return customer == null;
    }

    private IEnumerator CustomerMoveToCounter()
    {
        customerVisual.transform.position = customerVisual.transform.position + Vector3.forward * -2;
        customerVisual.SetActive(true);

        float travelPercent = 0;
        while (travelPercent < 1)
        {
            customerVisual.transform.position = Vector3.Lerp(customerVisual.transform.position, customerStartPosition, travelPercent);
            travelPercent += Time.deltaTime;
            Debug.Log(travelPercent);
            yield return new WaitForEndOfFrame();
        }
        customer = customerVisual;
    }
}
