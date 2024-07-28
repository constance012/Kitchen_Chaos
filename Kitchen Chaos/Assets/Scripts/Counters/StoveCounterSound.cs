using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
	[SerializeField] private StoveCounter stoveCounter;

	// Private fields.
	private AudioSource _audioSource;
	private float _warningSoundTimer;
	private bool _playWarningSound;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
		stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
	}

	private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
	{
		float burnShowProgressAmount = .5f;
		_playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
	}

	private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
	{
		bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
		if (playSound)
		{
			_audioSource.Play();
		}
		else
		{
			_audioSource.Pause();
		}
	}

	private void Update()
	{
		if (_playWarningSound)
		{
			_warningSoundTimer -= Time.deltaTime;
			if (_warningSoundTimer <= 0f)
			{
				float warningSoundTimerMax = .2f;
				_warningSoundTimer = warningSoundTimerMax;

				SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
			}
		}
	}

}