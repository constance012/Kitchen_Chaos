using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] private Transform counterTopPoint;

	public static event EventHandler OnAnyObjectPlacedHere;

	// Private fields.
	private KitchenObject _kitchenObject;

	public static void ResetStaticData()
	{
		OnAnyObjectPlacedHere = null;
	}

	public virtual void Interact(Player player)
	{
		Debug.LogError("BaseCounter.Interact();");
	}

	public virtual void InteractAlternate(Player player)
	{
		//Debug.LogError("BaseCounter.InteractAlternate();");
	}

	public Transform GetKitchenObjectFollowTransform()
	{
		return counterTopPoint;
	}

	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		if (kitchenObject != null)
		{
			_kitchenObject = kitchenObject;
			OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
		}
	}

	public KitchenObject GetKitchenObject()
	{
		return _kitchenObject;
	}

	public void ClearKitchenObject()
	{
		_kitchenObject = null;
	}

	public bool HasKitchenObject()
	{
		return _kitchenObject != null;
	}

}