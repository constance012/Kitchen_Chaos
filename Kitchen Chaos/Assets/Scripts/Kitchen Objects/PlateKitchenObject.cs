using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
	public class OnIngredientAddedEventArgs : EventArgs
	{
		public KitchenObjectSO kitchenObjectSO;
	}

	public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
	[SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

	// Private fields.
	private List<KitchenObjectSO> _kitchenObjectSOList;

	private void Awake()
	{
		_kitchenObjectSOList = new List<KitchenObjectSO>();
	}

	public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
	{
		if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
		{
			// Not a valid ingredient
			return false;
		}
		if (_kitchenObjectSOList.Contains(kitchenObjectSO))
		{
			// Already has this type
			return false;
		}
		else
		{
			_kitchenObjectSOList.Add(kitchenObjectSO);

			OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
			{
				kitchenObjectSO = kitchenObjectSO
			});

			return true;
		}
	}

	public List<KitchenObjectSO> GetKitchenObjectSOList()
	{
		return _kitchenObjectSOList;
	}
}