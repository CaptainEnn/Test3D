using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastEventTest : MonoBehaviour
{
    

    private int countVal;
    private string strVal = "Hello";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            countVal++;
            
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.LEFT_MOUSE_CLICK, countVal);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.RIGHT_MOUSE_CLICK, strVal);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerInfo playerInfo = new PlayerInfo()
            {
                PlayerName = "Nguyen",
                PlayerAge = 21
            };

            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_PLAYER_INFO, playerInfo);
            }

        }
    }
}
