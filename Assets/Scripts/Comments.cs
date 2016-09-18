using UnityEngine;
using System.Collections.Generic;

public static class Comments
{
    public static List<string>[] actions;
    static Comments()
    {
        actions = new List<string>[System.Enum.GetNames(typeof(CommentsEnum)).Length];

        #region COM_SHOOT_SUCCESS
        List<string> comShootSuccess = new List<string>();
        comShootSuccess.Add("Lovely pass to the striker and he scores!");
        comShootSuccess.Add("That's a shot and... a GOAL!");
        comShootSuccess.Add("Uses his head and the ball goes to the net!");
        comShootSuccess.Add("GOAL! Great finish from the striker!");
        comShootSuccess.Add("It's in! He lost the defender and finished it nicely!");
        comShootSuccess.Add("GOAL! What an ubelievable shot!");
        comShootSuccess.Add("Oh they've done it! That's a goal!");
        comShootSuccess.Add("Tries his luck... It's in!");
        comShootSuccess.Add("A GOAL! How on Earth that got in?!");
        comShootSuccess.Add("It's in! A world class finish!");
        comShootSuccess.Add("G-O-A-L!");
        comShootSuccess.Add("Goal! Goal! Goal!");
        comShootSuccess.Add("Gooooooooooooooaaaaaaaaal!!!");
        actions[(int)CommentsEnum.COM_SHOOT_SUCCESS] = comShootSuccess;
        #endregion

        #region COM_SHOOT_FAIL
        List<string> comShootFail = new List<string>();
        comShootFail.Add("Tries his luck! Oh it's gone wide...");
        comShootFail.Add("What a terrible shot...");
        comShootFail.Add("He shoots! Off the crossbar and out of play...");
        comShootFail.Add("Shot gone straight into the keeper's hands.");
        comShootFail.Add("Tries to shoot and misses the ball...");
        comShootFail.Add("He missed!");
        comShootFail.Add("Oh... That should've gone in!");
        comShootFail.Add("He hit the post!");
        comShootFail.Add("Recieved a nice pass... Shoots! Wide of target...");
        comShootFail.Add("Ohhh... How could he miss THAT?");
        comShootFail.Add("The weak shot couldn't change the result.");
        comShootFail.Add("The shot blocked by the defenders.");
        comShootFail.Add("The crowd looked disappointed - no goal.");
        actions[(int)CommentsEnum.COM_SHOOT_FAIL] = comShootFail;
        #endregion

        #region COM_PENALTY_SUCCESS
        List<string> comPenaltySuccess = new List<string>();
        comPenaltySuccess.Add("Runs up to the ball... It's in!");
        comPenaltySuccess.Add("Penalty taken... And we have a goal!");
        comPenaltySuccess.Add("Beautiful penalty!");
        comPenaltySuccess.Add("The keeper throws himself in the wrong way. Goal!");
        comPenaltySuccess.Add("Fooled the keeper! That's a goal!");
        comPenaltySuccess.Add("Panenka style! Lovely goal!");
        comPenaltySuccess.Add("Precise shot! Just next to the post!");
        comPenaltySuccess.Add("Keeper was close to get it but the shot was to good!");
        actions[(int)CommentsEnum.COM_PENALTY_SUCCESS] = comPenaltySuccess;
        #endregion

        #region COM_PENALTY_FAIL
        List<string> comPenaltyFail = new List<string>();
        comPenaltyFail.Add("Lousy shot and the keeper collects the ball.");
        comPenaltyFail.Add("Predictable shot! No goal from this penalty.");
        comPenaltyFail.Add("Good shot! But the keeper saves it!");
        comPenaltyFail.Add("Hit the post! No goal this time!");
        comPenaltyFail.Add("Awful penalty! Just like Beckham in 2004...");
        comPenaltyFail.Add("Too easy for the goalie. No goal.");
        comPenaltyFail.Add("The shot's gone wide! How could he miss?!");
        comPenaltyFail.Add("Wasted chance from the penalty!");
        comPenaltyFail.Add("Ooh... Great save from the keeper!");
        comPenaltyFail.Add("Missed it! He's going to be in trouble for that!");
        actions[(int)CommentsEnum.COM_PENALTY_FAIL] = comPenaltyFail;
        #endregion

        #region COM_ATTACK
        List<string> comAttack = new List<string>();
        comAttack.Add("The team advances through the field...");
        comAttack.Add("A lovely pass opens the chance.");
        comAttack.Add("Moving forward with the ball...");
        comAttack.Add("Dribbled past his opponent!");
        comAttack.Add("Used his feet nicely...");
        comAttack.Add("Won the ball in air with his head...");
        comAttack.Add("Nearly lost the ball in that tackle.");
        comAttack.Add("Showing some progress...");
        comAttack.Add("Has a chance to pass he ball...");
        comAttack.Add("Will he shoot from this range?");
        comAttack.Add("Played the ball on first contact.");
        comAttack.Add("Great technique allows him to advance.");
        actions[(int)CommentsEnum.COM_ATTACK] = comAttack;
        #endregion


        #region COM_FREEKICK_SUCCESS
        List<string> comFreekickSuccess = new List<string>();
        comFreekickSuccess.Add("It's in! Perfect free kick!");
        comFreekickSuccess.Add("Off the woodwork!");
        comFreekickSuccess.Add("GOAL! Just like Pirlo in the old days!");
        comFreekickSuccess.Add("The keeper won't reach it... A goal!");
        comFreekickSuccess.Add("Scoring with a precise shot!");
        comFreekickSuccess.Add("Brilliant! The keeper had no chance!");
        comFreekickSuccess.Add("Talking about a long-range goal!");
        comFreekickSuccess.Add("Nailed it just under the bar!");
        comFreekickSuccess.Add("He did it! He actually scored!");
        comFreekickSuccess.Add("GOAL! What a moment to score from a free kick!");
        comFreekickSuccess.Add("Just above the wall... It's in!!!");
        actions[(int)CommentsEnum.COM_FREEKICK_SUCCESS] = comFreekickSuccess;
        #endregion


        #region COM_FREEKICK_FAIL
        List<string> comFreekickFail = new List<string>();
        comFreekickFail.Add("Taking the free kick... Keeper gets it!");
        comFreekickFail.Add("Wasted free kick...");
        comFreekickFail.Add("Unfortunately, the shot's gone wide.");
        comFreekickFail.Add("Gone from the wrong side of the post...");
        comFreekickFail.Add("Horrible free kick! Could've hit a supporter!");
        comFreekickFail.Add("The ball hit the wall...");
        comFreekickFail.Add("That went completely wrong...");
        comFreekickFail.Add("This free kick won't turn into a goal...");
        actions[(int)CommentsEnum.COM_FREEKICK_FAIL] = comFreekickFail;
        #endregion


        #region COM_CORNER_SUCCESS
        List<string> comCornerSuccess = new List<string>();
        comCornerSuccess.Add("Good crossed ball into the box... And a goal!");
        comCornerSuccess.Add("The crossing found a teammate! It's in!!!");
        comCornerSuccess.Add("GOAL! Scored from a lovely header!");
        comCornerSuccess.Add("The ball swirls into the box... It's in the net!");
        comCornerSuccess.Add("Well taken corner! Ends with a goal!");
        comCornerSuccess.Add("Corner taken... Jumps above the defenders... GOAL!");
        comCornerSuccess.Add("Taking advantage from the set piece... It's in!");
        comCornerSuccess.Add("Played to the near post... He finished it!");
        comCornerSuccess.Add("Played onto the far post... Knocked it with the header!");
        actions[(int)CommentsEnum.COM_CORNER_SUCCESS] = comCornerSuccess;
        #endregion


        #region COM_CORNER_FAIL
        List<string> comCornerFail = new List<string>();
        comCornerFail.Add("Inaccurate corner. Ends with a loss...");
        comCornerFail.Add("The ball goes into the box... The defender clears it!");
        comCornerFail.Add("Well taken... He shoots! Blocked by the defender!");
        comCornerFail.Add("The header goes wide...");
        comCornerFail.Add("Straight into the hands of the keeper...");
        comCornerFail.Add("Taking the corner... And wastes it...");
        comCornerFail.Add("Too long... The ball leaves the pitch.");
        comCornerFail.Add("Cleared by the defenders.");
        comCornerFail.Add("The keeper punches it away!");
        actions[(int)CommentsEnum.COM_CORNER_FAIL] = comCornerFail;
        #endregion
    }

    public static void Log(CommentsEnum action)
    {
        string randomString = GetRandomStringFromList(actions[(int)action]);
        GameManager.instance.logs.AddText(randomString);
    }

    public static void Log(TreeAction action)
    {
        string randomString = GetRandomStringFromList(action.messages);
        GameManager.instance.logs.AddText(randomString);
    }

    private static string GetRandomStringFromList(List<string> list)
    {
        string[] s = list.ToArray();
        return s[Random.Range(0, s.Length)];
    }
}