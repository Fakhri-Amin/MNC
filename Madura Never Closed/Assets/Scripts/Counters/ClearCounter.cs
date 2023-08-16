using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasProductObject())
        {
            // There is no product object here
            if (player.HasProductObject())
            {
                // Player is carrying something
                player.GetProductObject().SetProductObjectParent(this);
            }
            else
            {
                // Player not caryying anything

            }
        }
        else
        {
            // There is product object here
            if (player.HasProductObject())
            {
                // Player is caryying something 
                if (player.GetProductObject().TryGetPlate(out PlateProductObject plateProductObject))
                {
                    // Player is holding a plate
                    if (plateProductObject.TryAddIngredient(GetProductObject().GetProductObjectSO()))
                    {
                        GetProductObject().DestroySelf();
                    }
                }
                else
                {
                    // Player is not carrying plate but something else
                    if (GetProductObject().TryGetPlate(out plateProductObject))
                    {
                        if (plateProductObject.TryAddIngredient(player.GetProductObject().GetProductObjectSO()))
                        {
                            player.GetProductObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetProductObject().SetProductObjectParent(player);
            }
        }
    }
}
