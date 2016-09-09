using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text startButtonText;
	public GameObject savePopUp;
	public GameObject[] profiles;
	public GameObject fade;
	public GameObject profileNamePopUp;
	public GameObject fadeAnchor;

	void Awake()
	{
		SaveLoad.LoadProfiles();
	}

	void Start ()
    {
        if (SaveLoad.SaveExists())
            startButtonText.text = "Continue";
        else
            startButtonText.text = "New Game";
		InitProfileDescriptions();
	}

	void InitProfileDescriptions()
	{
		for(int ii = 0; ii < profiles.Length; ii++)
		{
			if(SaveLoad.savedProfiles[ii].IsEmpty())
			{
				profiles[ii].transform.Find("ProfileName").GetComponent<Text>().text = "Empty";
				profiles[ii].transform.Find("LastSaveDate").GetComponent<Text>().text = "Empty";
				profiles[ii].transform.Find("ProfileInfo").GetComponent<Text>().text = "Empty";
				continue;
			}

			profiles[ii].transform.Find("ProfileName").GetComponent<Text>().text = SaveLoad.savedProfiles[ii].GetName();
			profiles[ii].transform.Find("LastSaveDate").GetComponent<Text>().text = SaveLoad.savedProfiles[ii].GetLastSaveTime().ToShortDateString() + "\n" + SaveLoad.savedProfiles[ii].GetLastSaveTime().ToShortTimeString();
			GameInformation data = SaveLoad.savedProfiles[ii].GetSession();
			profiles[ii].transform.Find("ProfileInfo").GetComponent<Text>().text = string.Format("{0}\n{1}\n{2}\n{3}",
				data.playerStats.playerName+" "+data.playerStats.playerSurname,
				data.playerStats.currentTeam.name,
				"Round "+data.currentRound,
				"Week "+data.currentWeekDay
			);
		}
	}

    public void NewGameClicked()
    {
		savePopUp.SetActive(true);
    }

	public void SetCurrentProfileName(string name)
	{
		if(name.Equals(""))
			return;
		SaveLoad.savedProfiles[SaveLoad.currentlySelectedProfile].SetName(name);
		StartNewGame();
	}

	public void ChooseSaveGame(int which)
	{
		SaveLoad.currentlySelectedProfile = which;
		if(SaveLoad.CheckIfProfileIsEmpty(which))
		{
			fade.transform.SetParent(fadeAnchor.transform);
			fade.SetActive(true);
			profileNamePopUp.SetActive(true);
		}
		else
			ContinueGameOnProfile(which);
	}

	void ContinueGameOnProfile(int which)
	{
		CareerManager.gameInfo = SaveLoad.LoadGame(which);
		SceneManager.LoadScene("playerMenu");
	}

	void StartNewGameOnProfile(int which)
	{
		SceneManager.LoadScene("charCreation");
	}

	public void QuitGameClicked()
	{
		Application.Quit();
	}

	public void StartNewGame()
	{
		SceneManager.LoadScene("charCreation");
	}
}
