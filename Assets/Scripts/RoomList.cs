using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomList : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private List<GameObject> allRooms;


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            //Debug.Log("RoomCreatedNumberOfRooms: " + roomList.Count.ToString() + " Counted " + i.ToString());
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                GameObject roomInstance = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                roomInstance.GetComponent<TMP_Text>().text = roomList[i].Name;

                allRooms.Add(roomInstance);
            }
            else
            {
                for (int j = 0; j < allRooms.Count; j++)
                {
                    if (allRooms[j].GetComponent<TMP_Text>().text == roomList[i].Name)
                    {
                        Destroy(allRooms[j]);
                        allRooms.Remove(allRooms[j]);
                    }
                }
            }

        }
    }
}
