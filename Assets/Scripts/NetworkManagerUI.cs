using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button joinRoomButton;
    [SerializeField] private TextMeshProUGUI roomCodeTextbox;

    private void Awake()
    {
        createRoomButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });

        joinRoomButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
