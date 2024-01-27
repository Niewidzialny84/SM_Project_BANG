using Photon.Pun;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject displayMove;
    public GameObject timer;
    public float time;
    public GameObject readyButton;
    private bool isAllPlayersReady;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAllPlayersReady();

        if (PhotonNetwork.PlayerList.Count() == 2 && isAllPlayersReady)
        {
            readyButton.SetActive(false);

            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time = 0;
            }

            DisplayTime(time);
        }

        if (time == 0)
            timer.SetActive(false);
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        isAllPlayersReady = false;
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

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        var seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer.GetComponent<Text>().text = seconds.ToString();
    }

    [PunRPC]
    public void GetReady()
    {
        var playersList = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in playersList)
        {
            if (player.GetComponent<PlayerScript>().playerName == PhotonNetwork.LocalPlayer.NickName)
            {
                player.GetComponent<PlayerScript>().GetReady();
            }
        }
    }

    private void CheckIfAllPlayersReady()
    {
        isAllPlayersReady = true;

        var playersList = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in playersList)
        {
            if (!player.GetComponent<PlayerScript>().isReady)
            {
                isAllPlayersReady = false;
            }
        }
    }
}
