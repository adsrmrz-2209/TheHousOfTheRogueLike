using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDefinitionSO", menuName = "Scriptable Objects/WeaponDefinitionSO")]
public class WeaponDefinitionSO : ScriptableObject
{
    public string weaponId;
    public WeaponType type;
    public List<WeaponStats> weaponStats;
}

[System.Serializable]
public class WeaponStats
{
    public float baseDamage;
    public float fireRate;
    public float reloadSpeed;
    public int magazineSize;
}
