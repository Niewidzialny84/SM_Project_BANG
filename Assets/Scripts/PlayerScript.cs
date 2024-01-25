using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //public GameObject player;
    public PhotonView photonView;
    public GameObject playerNameText;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            playerNameText.GetComponent<TMP_Text>().text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            playerNameText.GetComponent<TMP_Text>().text = photonView.Owner.NickName;
        }
    }
}
