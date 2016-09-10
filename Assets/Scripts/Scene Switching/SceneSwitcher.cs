using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour 
{
	public static SceneSwitcher instance;
	void Awake()
	{
		if(instance!=null)
			Destroy(instance.gameObject);
		if(instance==null)
			instance=this;
	}

	public void SwitchSceneTo(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void SwitchSceneToPlayerMenuAndCheckForLeagueEnd()
	{
		if(CareerManager.CheckForLeagueEnd())
			SceneManager.LoadScene("leagueEndStatistics");
		else
			SwitchSceneTo("playerMenu");
	}

}
