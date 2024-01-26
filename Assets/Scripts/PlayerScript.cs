using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject playerNameText;
    public GameObject readyButton;
    private bool wasFired;
    [SerializeField] private bool isReady;

    public float ShakeDetectionThreshold;
    private float sqrShakeDetectionThreshold;


    private void Awake()
    {
        wasFired = false;
        isReady = false;
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
            isReady = true;
        }
    }
}
