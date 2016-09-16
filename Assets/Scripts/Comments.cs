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
        comShootSuccess.Add("Przykladowy komentarz do dobrego strzalu");
        comShootSuccess.Add("Kolejny komentarz do dobrego strzalu");
        actions[(int)CommentsEnum.COM_SHOOT_SUCCESS] = comShootSuccess;
        #endregion

        #region COM_SHOOT_FAIL
        List<string> comShootFail = new List<string>();
        comShootFail.Add("Przykladowy komentarz do zlego strzalu");
        comShootFail.Add("Kolejny komentarz do zlego strzalu");
        actions[(int)CommentsEnum.COM_SHOOT_FAIL] = comShootFail;
        #endregion

        #region COM_PENALTY_SUCCESS
        List<string> comPenaltySuccess = new List<string>();
        comPenaltySuccess.Add("Przykladowy komentarz do dobrego karnego");
        comPenaltySuccess.Add("Kolejny komentarz do dobrego karnego");
        actions[(int)CommentsEnum.COM_PENALTY_SUCCESS] = comPenaltySuccess;
        #endregion

        #region COM_PENALTY_FAIL
        List<string> comPenaltyFail = new List<string>();
        comPenaltyFail.Add("Przykladowy komentarz do zlego karnego");
        comPenaltyFail.Add("Kolejny komentarz do zlego karnego");
        actions[(int)CommentsEnum.COM_PENALTY_FAIL] = comPenaltyFail;
        #endregion

        #region COM_ATTACK
        List<string> comAttack = new List<string>();
        comAttack.Add("Przykladowy komentarz do ataku");
        comAttack.Add("Kolejny komentarz do ataku");
        actions[(int)CommentsEnum.COM_ATTACK] = comAttack;
        #endregion


        #region COM_FREEKICK_SUCCESS
        List<string> comFreekickSuccess = new List<string>();
        comFreekickSuccess.Add("comment1");
        comFreekickSuccess.Add("comment2");
        actions[(int)CommentsEnum.COM_FREEKICK_SUCCESS] = comFreekickSuccess;
        #endregion


        #region COM_FREEKICK_FAIL
        List<string> comFreekickFail = new List<string>();
        comFreekickFail.Add("comment1");
        comFreekickFail.Add("comment2");
        actions[(int)CommentsEnum.COM_FREEKICK_FAIL] = comFreekickFail;
        #endregion


        #region COM_CORNER_SUCCESS
        List<string> comCornerSuccess = new List<string>();
        comCornerSuccess.Add("comment1");
        comCornerSuccess.Add("comment2");
        actions[(int)CommentsEnum.COM_CORNER_SUCCESS] = comCornerSuccess;
        #endregion


        #region COM_CORNER_FAIL
        List<string> comCornerFail = new List<string>();
        comCornerFail.Add("comment1");
        comCornerFail.Add("comment2");
        actions[(int)CommentsEnum.COM_CORNER_FAIL] = comCornerFail;
        #endregion
    }

    public static void Log(CommentsEnum action)
    {
        string randomString = GetRandomStringFromList(actions[(int)action]);
        GameManager.instance.logs.AddText(randomString);
    }

    private static string GetRandomStringFromList(List<string> list)
    {
        string[] s = list.ToArray();
        return s[Random.Range(0, s.Length)];
    }
}