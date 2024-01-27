using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    private GameObject displayMove;
    public GameObject timer;
    public GameObject result;
    public float time, timeDuel, timeToFinish;
    public GameObject readyButton;
    private bool isAllPlayersReady;
    private System.DateTime hourZero;
    private GameObject thisPlayer, otherPlayer;
    private bool gotTimeStamp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isAllPlayersReady) CheckIfAllPlayersReady();

        if (PhotonNetwork.PlayerList.Count() == 2 && isAllPlayersReady)
        {
            readyButton.SetActive(false);

            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else if (time <= 0)
            {
                if(!gotTimeStamp)
                {
                    hourZero = System.DateTime.UtcNow;
                    Handheld.Vibrate();
                    gotTimeStamp = true;
                }
                time = 0;

                if (timeDuel > 0)
                {
                    timeDuel -= Time.deltaTime;
                }
                else if (timeDuel <= 0)
                {
                    timeDuel = 0;

                    if(result.GetComponent<Text>().text == "")
                    {
                        var playersList = GameObject.FindGameObjectsWithTag("Player");
                        foreach (var player in playersList)
                        {
                            //Randomly we assign players only here, could be done earlier but needs some rebuilding. FOR NOW LEAVE AS IS
                            if (player.GetComponent<PlayerScript>().playerName == PhotonNetwork.LocalPlayer.NickName)
                            {
                                thisPlayer = player;
                            }
                            else
                            {
                                otherPlayer = player;
                            }
                        }

                        if (thisPlayer != null && !thisPlayer.GetComponent<PlayerScript>().wasFired)
                        {
                            result.GetComponent<Text>().text = "Not fired!";
                            result.GetComponent<Text>().color = Color.red;
                        }
                        else if (thisPlayer != null && hourZero.CompareTo(thisPlayer.GetComponent<PlayerScript>().fireTime) >= 0)
                        {
                            result.GetComponent<Text>().text = "Missfire!";
                            result.GetComponent<Text>().color = Color.red;
                        }
                        else if (thisPlayer != null && otherPlayer != null && !otherPlayer.GetComponent<PlayerScript>().wasFired)
                        {
                            result.GetComponent<Text>().text = "You win!";
                            result.GetComponent<Text>().color = Color.green;
                        }
                        else if (thisPlayer != null && otherPlayer != null && otherPlayer.GetComponent<PlayerScript>().wasFired && hourZero.CompareTo(otherPlayer.GetComponent<PlayerScript>().fireTime) >= 0)
                        {
                            result.GetComponent<Text>().text = "You win!";
                            result.GetComponent<Text>().color = Color.green;
                        }
                        else if (thisPlayer != null && otherPlayer != null && otherPlayer.GetComponent<PlayerScript>().wasFired)
                        {
                            if (thisPlayer.GetComponent<PlayerScript>().fireTime.CompareTo(otherPlayer.GetComponent<PlayerScript>().fireTime) >= 0)
                            {
                                result.GetComponent<Text>().text = "You died!";
                                result.GetComponent<Text>().color = Color.red;
                            }
                            else
                            {
                                result.GetComponent<Text>().text = "You win!";
                                result.GetComponent<Text>().color = Color.green;
                            }
                        }


                    }

                    if (timeToFinish > 0)
                    {
                        timeToFinish -= Time.deltaTime;
                    }
                    else if (timeToFinish <= 0 && PhotonNetwork.IsMasterClient)
                    {
                        var toKick = PhotonNetwork.PlayerListOthers;
                        foreach (var item in toKick) { PhotonNetwork.CloseConnection(item);  }
                        PhotonNetwork.Disconnect();
                    }
                    else
                    {
                        PhotonNetwork.Disconnect();
                    }
                }
            }

            DisplayTime(time);
        }

        if (time == 0)
            timer.SetActive(false);
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.Disconnect();
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        isAllPlayersReady = false;
        gotTimeStamp = false;
        timeDuel = 3;
        timeToFinish = 10;
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
