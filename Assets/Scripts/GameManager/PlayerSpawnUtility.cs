using UnityEngine;

public class PlayerSpawnUtility 
{
    public static void OverrideSpawnPos(Player playerData, Vector3 position)
    {
        playerData.SpawnPosition = new Vector3(position.x, position == Vector3.zero ? position.y : 2f,  position.z);
    }
}
