using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public string playerName;
    public PhotonView photonView;
    public GameObject playerNameText;
    private bool wasFired;
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
    }

    private void Update()
    {
        CheckIfShake();
    }

    private void CheckIfShake()
    {
        if (Input.acceleration.sqrMagnitude > sqrShakeDetectionThreshold && photonView.IsMine)
            wasFired = true;
    }

    public void GetReady()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("SetReady", RpcTarget.All);
        }
    }

    [PunRPC]
    public void SetReady() => isReady = true;
}
