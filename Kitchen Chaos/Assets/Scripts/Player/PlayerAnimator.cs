using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	[SerializeField] private Player player;
	
	private const string IS_WALKING = "IsWalking";

	// Private fields.
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		_animator.SetBool(IS_WALKING, player.IsWalking());
	}

}