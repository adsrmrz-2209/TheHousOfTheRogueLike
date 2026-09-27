using UnityEngine;

public interface IWeapon
{
    public string Name { get; set; }
    public float BaseDamage { get; set; }
    public float FireRate { get; set; }
    public int Bullets { get; set; }
    public float ReloadSpeed { get; set; }
    public int WeaponLevel { get; set; }

    public void Fire();
    public void Reload();
    public void Upgrade();
}
