using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlip : MonoBehaviour
{
    private GameObject wPanel = null;
    private GameObject bPanel = null;
    private Vector3 topPos;
    private Vector3 bottomPos;
    private TheWorld world = null;
    private char myColor;
    // Start is called before the first frame update
    void Start()
    {
        wPanel = GameObject.Find("WhitePanel");
        bPanel = GameObject.Find("BlackPanel");
        world = FindObjectOfType<TheWorld>();
        topPos = bPanel.transform.position;
        bottomPos = wPanel.transform.position;
        myColor = FindObjectOfType<NetworkManager>().myColor[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!world.useAI)
        {
            if (myColor == 'W')
            {
                wPanel.transform.position = bottomPos;
                bPanel.transform.position = topPos;
            }
            else
            {
                bPanel.transform.position = bottomPos;
                wPanel.transform.position = topPos;
            }
        }
    }
}
