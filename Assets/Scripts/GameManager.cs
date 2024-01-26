using Photon.Pun;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject displayMove;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        if (PhotonNetwork.PlayerList.Count() == 1)
        {
            displayMove = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, -300, 0), Quaternion.identity, 0);
        }
        else
        {
            displayMove = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 300, 0), Quaternion.identity, 0);
        }
    }
}
