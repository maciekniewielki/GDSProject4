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

		LeagueCalendar l=new LeagueCalendar(new Team[]{new Team("1"), new Team("2"), new Team("3"), new Team("4"), new Team("5"), new Team("6")});
		Debug.Log(l.ToString());
	}

	public void SwitchSceneTo(string name)
	{
		SceneManager.LoadScene(name);
	}

}
