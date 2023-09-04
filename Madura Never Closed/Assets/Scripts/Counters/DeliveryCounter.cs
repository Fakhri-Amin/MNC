using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance;

    private DeliveryManagerInCounter deliveryManagerInCounter;

    private void Awake()
    {
        Instance = this;
        deliveryManagerInCounter = GetComponent<DeliveryManagerInCounter>();
    }

    public override void Interact(Player player)
    {
        if (player.HasProductObject())
        {
            if (player.GetProductObject().TryGetPlate(out PlateProductObject plateProductObject))
            {
                // Only accepts plates

                deliveryManagerInCounter.DeliverRecipe(plateProductObject);

                player.GetProductObject().DestroySelf();
            }
        }
    }
}
