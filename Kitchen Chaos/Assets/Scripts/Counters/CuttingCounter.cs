using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
	[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
	
	public static event EventHandler OnAnyCut;
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
	public event EventHandler OnCut;

	// Private fields.
	private int _cuttingProgress;
	
	public new static void ResetStaticData()
	{
		OnAnyCut = null;
	}

	public override void Interact(Player player)
	{
		if (!HasKitchenObject())
		{
			// There is no KitchenObject here
			if (player.HasKitchenObject())
			{
				// Player is carrying something
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
				{
					// Player carrying something that can be Cut
					player.GetKitchenObject().SetKitchenObjectParent(this);
					_cuttingProgress = 0;

					CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
					{
						progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
					});
				}
			}
			else
			{
				// Player not carrying anything
			}
		}
		else
		{
			// There is a KitchenObject here
			if (player.HasKitchenObject())
			{
				// Player is carrying something
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
				{
					// Player is holding a Plate
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
					{
						GetKitchenObject().DestroySelf();
					}
				}
			}
			else
			{
				// Player is not carrying anything
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}

	public override void InteractAlternate(Player player)
	{
		if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
		{
			// There is a KitchenObject here AND it can be cut
			_cuttingProgress++;

			OnCut?.Invoke(this, EventArgs.Empty);
			OnAnyCut?.Invoke(this, EventArgs.Empty);

			CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

			OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
			{
				progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
			});

			if (_cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
			{
				KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

				GetKitchenObject().DestroySelf();

				KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
			}
		}
	}

	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
	{
		CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
		return cuttingRecipeSO != null;
	}


	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
	{
		CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
		if (cuttingRecipeSO != null)
		{
			return cuttingRecipeSO.output;
		}
		else
		{
			return null;
		}
	}

	private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
	{
		foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
		{
			if (cuttingRecipeSO.input == inputKitchenObjectSO)
			{
				return cuttingRecipeSO;
			}
		}
		return null;
	}
}