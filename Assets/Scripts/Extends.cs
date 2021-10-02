using UnityEngine;

public static class Extends
{
    public static bool IsPlayer(this GameObject gameObject)
    {
        return gameObject.name == "Player";
    }
}