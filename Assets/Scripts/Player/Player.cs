using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Player() { }

    public Player(PlayerBehavior player)
    {
        PlayerBehavior = player;
    }

    public float Health { get; set; } = 100f;
    public float Speed { get; set; } = 5.0f;
    public float JumpHeight { get; set; } = 1f;
    public List<IWeapon> Weapons { get; set; }
    public PlayerBehavior PlayerBehavior { get; set; }
    public Vector3 SpawnPosition { get; set; } = new Vector3(0, 2f, 0);
}
