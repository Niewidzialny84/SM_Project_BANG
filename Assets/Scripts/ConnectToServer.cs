using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public string[] sceneName;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(sceneName[0]);
        PhotonNetwork.JoinLobby();
    }

    //public override void OnJoinedLobby()
    //{

    //}

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(sceneName[1]);
        Destroy(GameObject.Find("SceneManager"));
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
