using UnityEngine;

[CreateAssetMenu(fileName = "New Burning Recipe", menuName = "Recipes/Burning")]
public class BurningRecipeSO : ScriptableObject
{
	public KitchenObjectSO input;
	public KitchenObjectSO output;
	public float burningTimerMax;
}