using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public WeaponDefinitionSO WeaponDefinition { get; }
    public WeaponStats Stats { get; set; }
    public string WeaponId { get; }
    public int CurrentAmmo { get; set; }
    public int WeaponLevel { get; set; }

    public void Fire();
    public void Reload();
    public void Upgrade();
    
}
