using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Moving,
    Idle
}
public class PlayerMove : MonoBehaviour
{
  
    public float speed=3;
    private CharacterDir dir;
    private CharacterController playerController;
    public bool isMoving=false;
    public PlayerState state = PlayerState.Idle;

    private PlayerAttack playerAttack;
    void Start()
    {
        dir = GetComponent<CharacterDir>();
        playerController = GetComponent<CharacterController>();
        playerAttack = this.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttack.state == NewPlayerState.ControlWalk )
        {
            float distance = Vector3.Distance(dir.target, transform.position);
            if (distance > 0.1f)
            {
                state = PlayerState.Moving;
                isMoving = true;
                playerController.SimpleMove(transform.forward * speed);
            }
            else
            {
                state = PlayerState.Idle;
                isMoving = false;
            }
        }
    



    }


    public void SimpleMove(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        playerController.SimpleMove(transform.forward * speed);
    }
}
