using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerName : MonoBehaviour
{
    private void Update()
    {
        this.GetComponent<RectTransform>().anchoredPosition = this.transform.parent.transform.parent.transform.position;
    }
}
