using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerDataHub : NetworkBehaviour
{
    public struct playerInfo
    {
        public Vector3 position;
        //public int pIndex;
        //add more as u go
        public playerInfo(Vector3 _position, int _pIndex)
        {
            position = _position;
            //pIndex = _pIndex;
        }
    }

    public class syncPlayer : SyncList<playerInfo> { }

    public syncPlayer players = new syncPlayer();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
