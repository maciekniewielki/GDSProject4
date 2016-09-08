using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad
{
	public static int currentlySelectedProfile;
	public static Profile[] savedProfiles;

    public static void SaveGame(GameInformation data)
    {
        BinaryFormatter b = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.dat");
		savedProfiles[currentlySelectedProfile].UpdateProfile(data);
		b.Serialize(file, savedProfiles);
        file.Close();
    }

    public static void SaveGame()
    {
        BinaryFormatter b = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.dat");
		savedProfiles[currentlySelectedProfile].UpdateProfile(CareerManager.gameInfo);
		b.Serialize(file, savedProfiles);
        file.Close();
		Debug.Log("Saved the game");
    }

	public static GameInformation LoadGame(int profileNumber)
    {
		if(!SaveExists()||savedProfiles==null)
			return null;
		
		return savedProfiles[profileNumber].GetSession();
    }

	public static void LoadProfiles()
	{
		if(!SaveExists())
		{
			savedProfiles = new Profile[4];
			for(int ii = 0; ii < savedProfiles.Length; ii++)
				savedProfiles[ii] = new Profile();

			return;
		}

		BinaryFormatter b = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/savedGames.dat", FileMode.Open);
		savedProfiles=(Profile[]) b.Deserialize(file);
		file.Close();
	}

    public static bool SaveExists()
    {
        return File.Exists(Application.persistentDataPath + "/savedGames.dat");
    }

	public static bool CheckIfProfileIsEmpty(int profileNumber)
	{
		return savedProfiles[profileNumber].IsEmpty();
	}
	
}
