using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI recipesDeliveredText;
	[SerializeField] private Button playAgainButton;

	private void Awake()
	{
		playAgainButton.onClick.AddListener(() =>
		{
			Loader.Load(Loader.Scene.MainMenuScene);
		});
	}

	private void Start()
	{
		KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

		Hide();
	}

	private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
	{
		if (KitchenGameManager.Instance.IsGameOver())
		{
			Show();

			recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
		}
		else
		{
			Hide();
		}
	}

	private void Show()
	{
		gameObject.SetActive(true);
		playAgainButton.Select();
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}