using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public PlayerData() { }

    public PlayerData(Player player)
    {
        Player = player;
    }

    public float Health { get; set; } = 100f;
    public float Speed { get; set; } = 5.0f;
    public float JumpHeight { get; set; } = 1f;
    public List<IWeapon> Weapons { get; set; }
    public Player Player { get; set; }
    public Vector3 SpawnPosition { get; set; } = new Vector3(0, 2f, 0);
}
