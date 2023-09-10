using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<ProductObjectSO> productObjectSOList;
    public string recipeName;
    public GameObject prefab;
    public Sprite customerIconSprite;
}
