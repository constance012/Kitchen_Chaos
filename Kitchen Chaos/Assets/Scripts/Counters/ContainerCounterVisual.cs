using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
	private const string OPEN_CLOSE = "OpenClose";

	[SerializeField] private ContainerCounter containerCounter;

	// Private fields.
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
	}

	private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
	{
		_animator.SetTrigger(OPEN_CLOSE);
	}
}