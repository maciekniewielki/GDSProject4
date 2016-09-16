using UnityEngine;
using System.Collections.Generic;

public static class Comments
{
    public static List<string>[] actions;
    static Comments()
    {
        List<List<string>> primalList = new List<List<string>>();

        #region COM_SHOOT_SUCCESS
        List<string> comShootSuccess = new List<string>();
        comShootSuccess.Add("Przykladowy komentarz do dobrego strzalu");
        comShootSuccess.Add("Kolejny komentarz do dobrego strzalu");
        primalList.Add(comShootSuccess);
        #endregion

        #region COM_SHOOT_FAIL
        List<string> comShootFail = new List<string>();
        comShootFail.Add("Przykladowy komentarz do zlego strzalu");
        comShootFail.Add("Kolejny komentarz do zlego strzalu");
        primalList.Add(comShootFail);
        #endregion

        #region COM_PENALTY_SUCCESS
        List<string> comPenaltySuccess = new List<string>();
        comPenaltySuccess.Add("Przykladowy komentarz do dobrego karnego");
        comPenaltySuccess.Add("Kolejny komentarz do dobrego karnego");
        primalList.Add(comPenaltySuccess);
        #endregion

        #region COM_PENALTY_FAIL
        List<string> comPenaltyFail = new List<string>();
        comPenaltyFail.Add("Przykladowy komentarz do zlego karnego");
        comPenaltyFail.Add("Kolejny komentarz do zlego karnego");
        primalList.Add(comPenaltyFail);
        #endregion

        #region COM_ATTACK
        List<string> comAttack = new List<string>();
        comAttack.Add("Przykladowy komentarz do ataku");
        comAttack.Add("Kolejny komentarz do ataku");
        primalList.Add(comAttack);
        #endregion

        actions = primalList.ToArray();

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