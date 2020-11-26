using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterMovement : NetworkBehaviour
{
    PlayerDataHub PDH;
    public int playerIndex;
    CharacterController CC;

    void Start()
    {

    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        PDH = GameObject.Find("PlayerData").GetComponent<PlayerDataHub>();

        CmdAssignIndex();
        if (!isLocalPlayer) { return; }
        this.gameObject.AddComponent<CharacterController>();
        CC = GetComponent<CharacterController>();
        CC.skinWidth = .001f;
    }

    [Command(ignoreAuthority = true)]
    void CmdAssignIndex()
    {
        RpcAssignIndex(Convert.ToInt32(netId));
    }
    [ClientRpc]
    void RpcAssignIndex(int ID)
    {
        playerIndex = ID-1;//the base netID is 1, and for this to work with list indecies i need it to start at zero...
    }

    public override void OnStopClient()
    {
        //PDH.players.RemoveAt(playerIndex);
        base.OnStopClient();
    }


    void Update()
    {
        if (!isLocalPlayer)
        {
            transform.position = Vector3.Lerp(transform.position,posLerp,Time.deltaTime * 40); //smooth movement over the network
            return;
        }
        Mover();
        //CmdUpdatePosition(transform.position);
    }

    Vector3 moveDirection;
    public float speed = 10;
    public float grav = 10;
    public float JumpHeight = 10;
    RaycastHit2D bodyHit;

    bool jumping;

    void Mover()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, 0);
        if(Input.GetKeyDown(KeyCode.W))
        { jumping = true; }
        if (Input.GetKeyUp(KeyCode.W))
        { jumping = false; }
    }

    private void FixedUpdate()
    {
        if (CC.isGrounded)
        {
            if (jumping)
            {
                moveDirection.y = JumpHeight;
            }
            else
            {
                moveDirection.y = -.01f;
            }
        }
        else
        {
            moveDirection.y -= grav;
        }
        
        moveDirection.x *= speed;
        CC.Move(moveDirection * Time.smoothDeltaTime);

        CmdUpdatePosition(transform.position);
    }

    

    [Command(ignoreAuthority = true)]
    void CmdUpdatePosition(Vector3 pos)
    {
        RpcUpdatePosition(pos);
    }

    Vector3 posLerp;

    [ClientRpc]
    void RpcUpdatePosition(Vector3 pos)
    {
        if (isLocalPlayer) { return; }
        posLerp = pos;
    }
}
