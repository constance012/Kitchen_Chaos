using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/Food Recipe")]
public class RecipeSO : ScriptableObject
{
	public List<KitchenObjectSO> kitchenObjectSOList;
	public string recipeName;

}