using UnityEngine;
using System.Collections;

public class SoundManager: MonoBehaviour
{
	public AudioSource goal;
	public AudioSource miss;
	public AudioSource kick;
	public AudioSource whistle;
	public AudioSource ambience;


	void Start()
	{
		GameManager g=GameManager.instance;
		g.onPlayerGoal+=goal.Play;
		g.onPlayerTeamGoal+=goal.Play;
		g.onEnemyTeamGoal+=goal.Play;
		g.onPlayerMiss+=miss.Play;
		g.onPlayerTeamMiss+=miss.Play;
		g.onEnemyTeamMiss+=miss.Play;
		g.player.onActionSuccess+=kick.Play;
		g.player.onActionFail+=kick.Play;
		g.onMatchStart+=ambience.Play;
		g.onHalfTime+=whistle.Play;
		g.onMatchEnd+=whistle.Play;
	}


}
