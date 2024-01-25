using Photon.Pun;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

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
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(-4, -2), Quaternion.identity, 0);
        else
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(-4, 2), Quaternion.identity, 0);
    }
}
