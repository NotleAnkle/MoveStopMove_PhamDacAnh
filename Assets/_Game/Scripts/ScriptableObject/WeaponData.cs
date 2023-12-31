using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] Weapon[] weaponPrefabs;

    public Weapon GetWeaponPrefab(WeaponType weaponType)
    {
        return weaponPrefabs[(int)weaponType - 1];
    }
}
