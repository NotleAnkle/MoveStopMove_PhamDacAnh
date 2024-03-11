﻿using _Framework.Event.Scripts;
using _UI.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private ParticleSystem reviveVFX;

    private bool IsAttacking = false;

    #region override
    public override void OnInit()
    {
        SumCoin();
        base.OnInit();
        SetSize(Constant.RANGE_DEFAULT);
        attackRange.OnInit();
        EquiedSkin();
        indicator.SetName("You");
        SetRotationDefault();
    }

    public override void PowerUp()
    {
        base.PowerUp();
        attackRange.OnInit();
        float rate = (this.Range - Constant.RANGE_DEFAULT) / (Constant.RANGE_MAX - Constant.RANGE_DEFAULT);
        CameraFollower.Instance.SetRateOffset(rate);
        SoundManager.Instance.Play(AudioType.SFX_SizeUp);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        SoundManager.Instance.Play(AudioType.SFX_PlayerDie);
        LevelManager.Instance.OnFail();
    }

    public void OnRevive()
    {
        reviveVFX.Play();
        IsDying = false;
        ChangeAnim(Constant.ANIM_IDLE);
    }
    #endregion

    void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            if (joystick.Direction != Vector2.zero)
            {
                Run();
            }
            else
            {
                if (IsHasTarget)
                {
                    if (IsAttackable)
                    {
                        Attack();
                    }
                }
                else
                {
                    Idle();
                }
            }
        }
        
    }

    #region action
    private void Run()
    {
        CancelAttack();
        Vector2 deltaPos = joystick.Direction * speed * Time.deltaTime;
        Vector3 nextPos = new Vector3(deltaPos.x, 0f, deltaPos.y) + transform.position;

        transform.position = CheckGround(nextPos);
        ChangeAnim(Constant.ANIM_RUN);

        Turn();
    }
    private void Idle()
    {
        ChangeAnim(Constant.ANIM_IDLE);
    }
    public void Attack()
    {
            IsAttacking = true;
            ChangeAnim(Constant.ANIM_ATTACK);
            Vector3 pos = GetFirstTargetPos();
            TurnTo(pos);
            StartCoroutine(Throw(pos));
    }
    public void CancelAttack()
    {
        IsAttacking = false;
    }
    private IEnumerator Throw(Vector3 target)
    {
        yield return new WaitForSeconds(Constant.TIME_ATTACK_DELAY);
        if (IsAttacking)
        {
            curWeapon.Throw(target);
            SoundManager.Instance.Play(AudioType.SFX_ThrowWeapon);
        }
        IsAttacking = false;

        yield return new WaitForSeconds(0.1f);
        Idle();
    }

    //quay model theo joystick
    private void Turn()
    {
        currentSkin.TF.forward = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y);
    }
    private Vector3 CheckGround(Vector3 nextPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(nextPos + Vector3.up * 1.5f, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point;
        }
        return TF.position;
    }
    #endregion

    #region skin
    public override void EquipedCloth()
    {
        base.EquipedCloth();
        WearEquipedCloth();
        curWeapon.OnInit(this);
    }
    private void EquiedSkin()
    {
        ChangeSkin(UserData.Ins.GetEnumData(UserData.Key_Player_Skin, SkinType.Normal));
    }
    public void TryCloth(UIShop.ShopType shopType, Enum type)
    {
        switch (shopType)
        {
            case UIShop.ShopType.hair:
                currentSkin.ChangeHair((HairType)type);
                break;
            case UIShop.ShopType.pant:
                currentSkin.ChangePant((PantType)type);
                break;
            case UIShop.ShopType.accessory:
                currentSkin.ChangeAccessory((AccessoryType)type);
                break;
            case UIShop.ShopType.skin:
                ChangeSkin((SkinType)type);
                break;
            case UIShop.ShopType.weapon:
                ChangeWeapon((WeaponType)type);
                break;
        }
    }
    public void WearEquipedCloth()
    {
        ChangeHair(UserData.Ins.GetEnumData(UserData.Key_Player_Hair, HairType.None));
        ChangePant(UserData.Ins.GetEnumData(UserData.Key_Player_Pant, PantType.BatMan));
        ChangeAccessory(UserData.Ins.GetEnumData(UserData.Key_Player_Accessory, AccessoryType.None));

        ChangeWeapon(UserData.Ins.GetEnumData(UserData.Key_Player_Weapon, WeaponType.Kinfe));
    }
    #endregion

    private void SumCoin()
    {
        int coin = UserData.Ins.coin;
        coin += Score;
        UserData.Ins.SetIntData(UserData.Key_Coin,ref UserData.Ins.coin, coin);
    }
}
