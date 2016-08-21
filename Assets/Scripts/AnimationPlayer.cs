using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationPlayer : MonoBehaviour {

	private Animator animator;
	private Player player;
	private Dictionary<string, string[]> animationGroups;

	// Use this for initialization
	void Start () 
	{
		animationGroups=new Dictionary<string, string[]>();
		animator=GetComponent<Animator>();
		player=GameManager.instance.player;
		GameManager.instance.onActionTreeSetPieceEnd+=OnPlayerSetPieceActionSuccess;
		GameManager.instance.onActionTreeNormalActionEnd+=OnPlayerNormalActionSuccess;
		GameManager.instance.playAnimation+=PlayAnimation;

		animationGroups.Add("free_kick_successful", new string[]{"free_kick_successful"});
		animationGroups.Add("celny_gol", new string[]{"celny_gol_1", "celny_gol_2"});
        animationGroups.Add("penalties_succesful", new string[] { "celny_karny_1", "celny_karny_2" });
        animationGroups.Add("dribbling_succesful", new string[] { "udany_drybling" });
        animationGroups.Add("foul_normal", new string[] {"foul_1"});
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameManager.instance.IsGameHardPaused())
		{
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("no_animation")&&animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1.0f)
			{
				GameManager.instance.logs.FlushTheBuffer();
				GameManager.instance.HardUnpause();
				animator.Play("no_animation");
			}
		}
	}

	void OnPlayerSetPieceActionSuccess()
	{
		if(player.actionCompleted.Equals("FreeKick"))
		{
			GameManager.instance.HardPause();
			animator.Play("free_kick_successful");
		}
	}

	void OnPlayerNormalActionSuccess()
	{
		
	}

	string GetRandomStringByGroupName(string groupName)
	{
		string[] names=animationGroups[groupName];
		return names[Random.Range(0, names.Length)];
	}

	void PlayAnimation(string animationGroupName)
	{
		string name=GetRandomStringByGroupName(animationGroupName);
		animator.Play(name);
		GameManager.instance.HardPause();
	}
}
