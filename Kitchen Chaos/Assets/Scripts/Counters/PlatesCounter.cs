using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
	[SerializeField] private KitchenObjectSO plateKitchenObjectSO;
	
	public event EventHandler OnPlateSpawned;
	public event EventHandler OnPlateRemoved;

	// Private fields.
	private float _spawnPlateTimer;
	private float _spawnPlateTimerMax = 4f;
	private int _platesSpawnedAmount;
	private int _platesSpawnedAmountMax = 4;

	private void Update()
	{
		_spawnPlateTimer += Time.deltaTime;
		if (_spawnPlateTimer > _spawnPlateTimerMax)
		{
			_spawnPlateTimer = 0f;

			if (KitchenGameManager.Instance.IsGamePlaying() && _platesSpawnedAmount < _platesSpawnedAmountMax)
			{
				_platesSpawnedAmount++;

				OnPlateSpawned?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public override void Interact(Player player)
	{
		if (!player.HasKitchenObject())
		{
			// Player is empty handed
			if (_platesSpawnedAmount > 0)
			{
				// There's at least one plate here
				_platesSpawnedAmount--;

				KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

				OnPlateRemoved?.Invoke(this, EventArgs.Empty);
			}
		}
	}

}