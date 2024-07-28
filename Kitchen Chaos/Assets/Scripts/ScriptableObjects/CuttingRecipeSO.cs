using UnityEngine;

[CreateAssetMenu(fileName = "New Cutting Recipe", menuName = "Recipes/Cutting")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingProgressMax;
}