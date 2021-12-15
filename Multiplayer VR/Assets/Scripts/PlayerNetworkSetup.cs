using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    public GameObject localXRRigGameobject;

    public GameObject avatarHeadGameobject;
    public GameObject avatarBodyGameobject;

    void Start()
    {
        if(photonView.IsMine)
        {
            //the player is local(me)
            localXRRigGameobject.SetActive(true);

            SetLayerRecursively(avatarHeadGameobject, 6);
            SetLayerRecursively(avatarBodyGameobject, 7);

            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
            if(teleportationAreas.Length > 0)
            {
                Debug.Log("Found " + teleportationAreas.Length + " teleportation area. ");
                foreach (var item in teleportationAreas)
                {
                    item.teleportationProvider = localXRRigGameobject.GetComponent<TeleportationProvider>();
                }
            }
        }
        else
        {
            //the player is remote
            localXRRigGameobject.SetActive(false);

            SetLayerRecursively(avatarHeadGameobject, 0);
            SetLayerRecursively(avatarBodyGameobject, 0);
        }
    }

    void Update()
    {
        
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
