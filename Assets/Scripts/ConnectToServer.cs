using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public string sceneName;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(sceneName);
        PhotonNetwork.JoinLobby();
    }

    //public override void OnJoinedLobby()
    //{
        
    //}

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
