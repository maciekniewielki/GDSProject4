using UnityEngine;

public static class ExtensionMethods
{

    public static string GetRandomString(this string[] table)
    {
        return table[Random.Range(0, table.Length)];
    }
}
