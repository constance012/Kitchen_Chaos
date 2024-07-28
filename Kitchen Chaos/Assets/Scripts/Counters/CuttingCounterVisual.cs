using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
	private const string CUT = "Cut";

	[SerializeField] private CuttingCounter cuttingCounter;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		cuttingCounter.OnCut += CuttingCounter_OnCut;
	}

	private void CuttingCounter_OnCut(object sender, System.EventArgs e)
	{
		_animator.SetTrigger(CUT);
	}
}