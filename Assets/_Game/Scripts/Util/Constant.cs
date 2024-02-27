using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public const string TAG_PLAYER = "Player";
    public const string TAG_BOT = "Bot";

    public const string ANIM_IDLE = "idle";
    public const string ANIM_RUN = "run";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_DANCE_WIN = "win";
    public const string ANIM_DEAD = "dead";
    public const string ANIM_DANCE = "run";

    public const float RANGE_DEFAULT = 8f;

    public const float TIME_ATTACK_DELAY = 0.3f;
}

public enum PantType
{
    None = 0,
    BatMan ,
    ChamBi ,
    Comy,
    DaBao,
    Oninon,
    Pokemon,
    Rainbow,
    Skull,
    VanTim,
}

public enum WeaponType
{
    None = 0,
    Hammer = PoolType.W_Hammer,
    Kinfe = PoolType.W_Kinfe,
    Boomerang = PoolType.W_Boomerang,
}

public enum HairType
{
    None = 0,
    Arrow = PoolType.H_Arrow,
    Cowboy = PoolType.H_Cowboy,
    Crown = PoolType.H_Crown,
    Ear = PoolType.H_Ear,
    Flower = PoolType.H_Flower,
    Hair = PoolType.H_Hair,
    Hat = PoolType.H_Hat,
    Hat_Cap = PoolType.H_Hat_Cap,
    Hat_Yellow = PoolType.H_Hat_Yellow,
    Headphone = PoolType.H_Headphone,
    Horn = PoolType.H_Horn,
    Rau = PoolType.H_Rau,
}

public enum AccessoryType
{
    None = 0,
    Shield_1 = PoolType.ACC_Shield_1,
    Shield_2 = PoolType.ACC_Shield_2,
}
public enum SkinType
{
    Norman = 0
}