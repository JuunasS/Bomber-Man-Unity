using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class WaitForPlayers : MonoBehaviourPunCallbacks
{
    public Canvas WaitCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        CheckState();
    }

    [PunRPC]
    public void CheckState()
    {
        Debug.Log("Checking if there are enough players");
        if (GameManager.manager.CountPlayers() < 2)
        {
            WaitCanvas.gameObject.SetActive(true);
            GameManager.manager.isGameStarted = false;
        } else
        {
            WaitCanvas.gameObject.SetActive(false);
            GameManager.manager.isGameStarted = true;
        }
    }
}
