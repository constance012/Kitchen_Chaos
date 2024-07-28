using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
	[SerializeField] private RecipeListSO recipeListSO;

	public static DeliveryManager Instance { get; private set; }
	
	public event EventHandler OnRecipeSpawned;
	public event EventHandler OnRecipeCompleted;
	public event EventHandler OnRecipeSuccess;
	public event EventHandler OnRecipeFailed;

	// Private fields.
	private List<RecipeSO> _waitingRecipeSOList;
	private float _spawnRecipeTimer;
	private float _spawnRecipeTimerMax = 4f;
	private int _waitingRecipesMax = 4;
	private int _successfulRecipesAmount;


	private void Awake()
	{
		Instance = this;


		_waitingRecipeSOList = new List<RecipeSO>();
	}

	private void Update()
	{
		_spawnRecipeTimer -= Time.deltaTime;
		if (_spawnRecipeTimer <= 0f)
		{
			_spawnRecipeTimer = _spawnRecipeTimerMax;

			if (KitchenGameManager.Instance.IsGamePlaying() && _waitingRecipeSOList.Count < _waitingRecipesMax)
			{
				RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

				_waitingRecipeSOList.Add(waitingRecipeSO);

				OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
	{
		for (int i = 0; i < _waitingRecipeSOList.Count; i++)
		{
			RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];

			if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
			{
				// Has the same number of ingredients
				bool plateContentsMatchesRecipe = true;
				foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
				{
					// Cycling through all ingredients in the Recipe
					bool ingredientFound = false;
					foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
					{
						// Cycling through all ingredients in the Plate
						if (plateKitchenObjectSO == recipeKitchenObjectSO)
						{
							// Ingredient matches!
							ingredientFound = true;
							break;
						}
					}
					if (!ingredientFound)
					{
						// This Recipe ingredient was not found on the Plate
						plateContentsMatchesRecipe = false;
					}
				}

				if (plateContentsMatchesRecipe)
				{
					// Player delivered the correct recipe!

					_successfulRecipesAmount++;

					_waitingRecipeSOList.RemoveAt(i);

					OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
					OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
					return;
				}
			}
		}

		// No matches found!
		// Player did not deliver a correct recipe
		OnRecipeFailed?.Invoke(this, EventArgs.Empty);
	}

	public List<RecipeSO> GetWaitingRecipeSOList()
	{
		return _waitingRecipeSOList;
	}

	public int GetSuccessfulRecipesAmount()
	{
		return _successfulRecipesAmount;
	}

}
