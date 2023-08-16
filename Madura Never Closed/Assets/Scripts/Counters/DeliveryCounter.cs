using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance;

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasProductObject())
        {
            if (player.GetProductObject().TryGetPlate(out PlateProductObject plateProductObject))
            {
                // Only accepts plates

                DeliveryManager.Instance.DeliverRecipe(plateProductObject);

                player.GetProductObject().DestroySelf();
            }
        }
    }
}
