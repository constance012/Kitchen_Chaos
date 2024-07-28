using UnityEngine.SceneManagement;

public static class Loader
{
	public enum Scene
	{
		MainMenuScene,
		GameScene,
		LoadingScene
	}

	private static Scene _targetScene;

	public static void Load(Scene targetScene)
	{
		_targetScene = targetScene;

		SceneManager.LoadSceneAsync(Scene.LoadingScene.ToString());
	}

	public static void LoaderCallback()
	{
		SceneManager.LoadSceneAsync(_targetScene.ToString());
	}

}