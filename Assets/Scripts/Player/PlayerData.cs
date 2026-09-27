using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float Health { get; set; } = 100f;
    public float Speed { get; set; } = 5.0f;
    public float JumpHeight { get; set; }
    public List<IWeapon> Weapons { get; set; }
}
