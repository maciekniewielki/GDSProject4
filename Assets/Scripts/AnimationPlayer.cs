using UnityEngine;
using System.Collections;

public class AnimationPlayer : MonoBehaviour {

	private Animator animator;
	private Player player;
	// Use this for initialization
	void Start () 
	{
		animator=GetComponent<Animator>();
		player=GameManager.instance.player;
		player.onActionSuccess+=OnPlayerActionSuccess;
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

	void OnPlayerActionSuccess()
	{
		if(player.actionCompleted.Equals("FreeKick"))
		{
			GameManager.instance.HardPause();
			animator.Play("free_kick_successful");
		}
	}
}
