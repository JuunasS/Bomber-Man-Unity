using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyPowerup : MonoBehaviourPunCallbacks
{
    public void Destroy()
    {
        photonView.RPC("DestroyRPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void DestroyRPC()
    {
        Debug.Log("Destroying powerup");
        Destroy(this.gameObject);
    }
}
