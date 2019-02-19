﻿using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class Run_Rifle : WeaponState
{
    [Header("[Components]")]
    public C_Velocity _velocity;
    public C_IKManager _iKManager;
    public C_WeaponHandle _weaponHandle;
    public WeaponAttribute _weaponAttribute;


    [Header("[Extra Properties]")]
    public TransformMark runOffset;

    public override void Init(GameObject obj)
    {
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _weaponHandle = obj.GetComponent<C_WeaponHandle>();
        _weaponAttribute = GetComponent<WeaponAttribute>();

    }

    public override bool Listener() {

        if (_velocity.Drun && !_velocity.jumping)
        {
            if (!_weaponAttribute.reload)
            {
                return true;
            }
        }

        return false;
    }


    public override void Enter() {
        _iKManager.SetAim(false);
        _weaponHandle.handPoint.localPosition = runOffset._position;
        _weaponHandle.handPoint.localEulerAngles = runOffset._rotation;

    }

    public override void OnUpdate()
    {
        
        if (!_velocity.Drun || _velocity.jumping)
        {
            this._exitTick = true;
        }
    }
    public override void Exit() {
        _iKManager.SetAim(true);
        _weaponHandle.handPoint.localPosition = _weaponAttribute.holdOffset._position;
        _weaponHandle.handPoint.localEulerAngles = _weaponAttribute.holdOffset._rotation;
    }
}
 