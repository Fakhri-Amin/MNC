using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler<OnRecipeSpawnedEventArgs> OnRecipeSpawned;
    public class OnRecipeSpawnedEventArgs
    {
        public int counterNumber;
    }
    public event EventHandler<OnRecipeCompeletedEventArgs> OnRecipeCompeleted;
    public class OnRecipeCompeletedEventArgs
    {
        public int counterNumber;
    }

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnCustomerSpawned;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private List<DeliveryManagerInCounter> deliveryManagerInCounterList;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private readonly float spawnRecipeTimerMax = 4f;

    private int successfulRecipeNumber;
    private DeliveryManagerInCounter deliveryCounter;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
        for (int i = 0; i < deliveryManagerInCounterList.Count; i++)
        {
            deliveryManagerInCounterList[i].SetCounterNumber(i + 1);
        }
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < deliveryManagerInCounterList.Count + 1 && CanSpawnCustomerOnDeliveryCounter())
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                SpawnCustomerOnDeliveryCounter();
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, new OnRecipeSpawnedEventArgs
                {
                    counterNumber = deliveryCounter.GetCounterNumber()
                });
            }
        }
    }

    private bool CanSpawnCustomerOnDeliveryCounter()
    {
        deliveryCounter = deliveryManagerInCounterList[UnityEngine.Random.Range(0, deliveryManagerInCounterList.Count)];
        if (deliveryCounter.IsCustomerEmpty())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpawnCustomerOnDeliveryCounter()
    {
        deliveryCounter.SpawnCustomer();
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipeNumber()
    {
        return successfulRecipeNumber;
    }

    public void IncrementSuccessRecipeNumber()
    {
        successfulRecipeNumber++;
    }

    public void RemoveRecipeSOFromList(int index)
    {
        waitingRecipeSOList.RemoveAt(index);
    }

    public void FireOnRecipeSuccessEvent()
    {
        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
    }

    public void FireOnRecipeFailedEvent()
    {
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public void FireOnDeliveryManagerCompletedEvent()
    {
        OnRecipeCompeleted?.Invoke(this, new OnRecipeCompeletedEventArgs
        {
            counterNumber = deliveryCounter.GetCounterNumber()
        });
    }

}
