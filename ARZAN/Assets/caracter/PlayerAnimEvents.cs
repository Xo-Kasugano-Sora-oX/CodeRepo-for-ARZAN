using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : Entity
{
    private PlayerController player;
    protected override void Start()

    {
        base.Start();
        player = GetComponentInParent<PlayerController>();
    }
    private void AnimationTrgger()
    {
        player.AttackOver();
    }
}
