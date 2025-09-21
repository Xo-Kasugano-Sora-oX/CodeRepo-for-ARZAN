using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }
    private void AnimationTrgger()
    {
        player.AttackOver();
    }
}
