using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button joinRoomButton;
    [SerializeField] private GameObject roomCodeTextbox;
    [SerializeField] private string sceneName;
    private GameObject errorText;

    public void CreateRoom()
    {
        if (roomCodeTextbox.GetComponent<TMP_InputField>().text != "")
        {
            //Debug.Log(roomCodeTextbox.GetComponent<TMP_InputField>().text);
            errorText = GameObject.FindGameObjectWithTag("ErrorMSG");
            if (errorText is not null)
            {
                errorText.GetComponent<TMP_Text>().text = "";
            }
            PhotonNetwork.CreateRoom(roomCodeTextbox.GetComponent<TMP_InputField>().text, new RoomOptions() { MaxPlayers = 2, IsVisible = true, IsOpen = true }, TypedLobby.Default, null);
        }
        else
        {
            Debug.Log("ERROR: No Room name!");
            errorText = GameObject.FindGameObjectWithTag("ErrorMSG");
            if (errorText is not null)
            {
                errorText.GetComponent<TMP_Text>().text = "Missing room code";
            }
        }
    }

    public void JoinRoom()
    {
        if (roomCodeTextbox.GetComponent<TMP_InputField>().text != "")
        {
            PhotonNetwork.JoinRoom(roomCodeTextbox.GetComponent<TMP_InputField>().text);
        }
        else
        {
            Debug.Log("ERROR: No Room name!");
            errorText = GameObject.FindGameObjectWithTag("ErrorMSG");
            if (errorText is not null)
            {
                errorText.GetComponent<TMP_Text>().text = "Missing room code";
            }
        }    
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
