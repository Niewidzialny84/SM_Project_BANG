using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject playerNameText;
    private bool wasFired;

    public float ShakeDetectionThreshold;
    private float sqrShakeDetectionThreshold;


    private void Awake()
    {
        wasFired = false;
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);

        if (photonView.IsMine)
        {
            playerNameText.GetComponent<TMP_Text>().text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            playerNameText.GetComponent<TMP_Text>().text = photonView.Owner.NickName;
        }
    }

    void Update()
    {
        if (Input.acceleration.sqrMagnitude > sqrShakeDetectionThreshold && photonView.IsMine)
        {
            wasFired = true;
        }
    }
}
