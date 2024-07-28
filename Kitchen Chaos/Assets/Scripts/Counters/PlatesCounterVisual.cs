using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
	[SerializeField] private PlatesCounter platesCounter;
	[SerializeField] private Transform counterTopPoint;
	[SerializeField] private Transform plateVisualPrefab;

	// Private fields.
	private List<GameObject> _plateVisualGameObjectList;

	private void Awake()
	{
		_plateVisualGameObjectList = new List<GameObject>();
	}

	private void Start()
	{
		platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
		platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
	}

	private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
	{
		GameObject plateGameObject = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];
		_plateVisualGameObjectList.Remove(plateGameObject);
		Destroy(plateGameObject);
	}

	private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
	{
		Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

		float plateOffsetY = .1f;
		plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualGameObjectList.Count, 0);

		_plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
	}

}