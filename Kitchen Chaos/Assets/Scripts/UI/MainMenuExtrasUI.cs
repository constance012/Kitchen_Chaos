using UnityEngine;
using UnityEngine.UI;

public class MainMenuExtrasUI : MonoBehaviour
{
	[SerializeField] private Button youTubeButton;

	private void Awake()
	{
		youTubeButton.onClick.AddListener(() =>
		{
			Application.OpenURL("https://www.youtube.com/watch?v=AmGSEH7QcDg");
		});
	}
}