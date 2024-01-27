using Photon.Pun;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public string playerName;
    public PhotonView photonView;
    public GameObject playerNameText;
    public GameObject controller;
    public bool wasFired;
    public System.DateTime fireTime;
    public bool isReady;

    public float ShakeDetectionThreshold;
    private float sqrShakeDetectionThreshold;


    private void Awake()
    {
        wasFired = false;
        isReady = false;
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);

        if (photonView.IsMine)
        {
            playerName = PhotonNetwork.LocalPlayer.NickName;
            playerNameText.GetComponent<TMP_Text>().text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            playerName = photonView.Owner.NickName;
            playerNameText.GetComponent<TMP_Text>().text = photonView.Owner.NickName;
        }
        controller = FindObjectOfType<GameManager>().gameObject;
    }

    private void Update()
    {
        if (/*Input.GetKey("space") &&*/ controller.GetComponent<GameManager>().time <= 3f && controller.GetComponent<GameManager>().timeDuel > 0f && !wasFired && photonView.IsMine)
        {
            //photonView.RPC("ShootingAction", RpcTarget.All);
            CheckIfShake();
        }

    }

    private void CheckIfShake()
    {
        if (Input.acceleration.sqrMagnitude > sqrShakeDetectionThreshold && photonView.IsMine)
            photonView.RPC("ShootingAction", RpcTarget.All);
    }

    public void GetReady()
    {
        if (photonView.IsMine && PhotonNetwork.PlayerList.Count() == 2)
        {
            photonView.RPC("SetReady", RpcTarget.All);
        }
    }

    [PunRPC]
    public void SetReady() => isReady = true;

    [PunRPC]
    public void ShootingAction()
    {
        wasFired = true;
        fireTime = System.DateTime.UtcNow;
    }

    private void OnDestroy()
    {
        if(photonView.IsMine)
        PhotonNetwork.Destroy(this.gameObject);
    }
}
