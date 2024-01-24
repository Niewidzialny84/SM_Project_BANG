using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagerUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button joinRoomButton;
    [SerializeField] private TextMeshProUGUI roomCodeTextbox;
    [SerializeField] private string sceneName;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomCodeTextbox.text, new RoomOptions() {MaxPlayers = 2, IsVisible = true, IsOpen = true}, TypedLobby.Default, null);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomCodeTextbox.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
