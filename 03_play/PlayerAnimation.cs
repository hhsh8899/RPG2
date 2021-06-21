using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMove move;
    private Animator player_ani;

    public PlayerAttack playerAttack;

    void Start()
    {
        move = GetComponent<PlayerMove>();
        player_ani = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerAttack.state == NewPlayerState.ControlWalk)
        {
            AnimatorStateInfo stateInfo = player_ani.GetCurrentAnimatorStateInfo(0);
            if (move.state == PlayerState.Moving)
            {//if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.idle") && !player_ani.IsInTransition(0))

                player_ani.SetBool("normalAttack", false);
                player_ani.SetBool("run", true);

            }
            else if (move.state == PlayerState.Idle)
            {

                player_ani.SetBool("run", false);


            }
        }
      else if (playerAttack.state == NewPlayerState.NormalAttack)
        {
            if (playerAttack.attackState == AttackState.Moving)
            {
               
                player_ani.SetBool("run", true);
            }
        }
    }
}
