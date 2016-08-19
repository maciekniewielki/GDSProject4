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

		LeagueCalendar l=new LeagueCalendar(new Team[]{new Team("Haha"), new Team("Hehe"), new Team("Hihi"), new Team("Huhu")});
	}

	public void SwitchSceneTo(string name)
	{
		SceneManager.LoadScene(name);
	}

}
