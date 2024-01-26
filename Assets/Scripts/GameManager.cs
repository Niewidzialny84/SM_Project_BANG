using Photon.Pun;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject displayMove;
    public GameObject timer;
    public float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.PlayerList.Count() == 1)
        {
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

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        var seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer.GetComponent<Text>().text = seconds.ToString();
    }
}
