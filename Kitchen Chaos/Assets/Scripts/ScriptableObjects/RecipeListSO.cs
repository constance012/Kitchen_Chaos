using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe List", menuName = "Recipes/Food Recipe List")]
public class RecipeListSO : ScriptableObject {

    public List<RecipeSO> recipeSOList;

}