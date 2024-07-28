using UnityEngine;

[CreateAssetMenu(fileName = "New Frying Recipe", menuName = "Recipes/Frying")]
public class FryingRecipeSO : ScriptableObject
{
	public KitchenObjectSO input;
	public KitchenObjectSO output;
	public float fryingTimerMax;
}