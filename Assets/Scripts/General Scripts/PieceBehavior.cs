using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehavior : MonoBehaviour
{
    public TheWorld world = null;
    private Vector3 myRote;

    private void Start()
    {
        world = FindObjectOfType<TheWorld>();
        myRote = transform.rotation.eulerAngles;
        if(FindObjectOfType<NetworkManager>() != null)
        {
            myRote.z = FindObjectOfType<NetworkManager>().myColor[0] == 'W' ? 0 : (float)180;
        }
        transform.eulerAngles = myRote;
    }
    // Update is called once per frame
    void Update()
    {
        if(!world.useAI && world.isSinglePlayer)
        {
            myRote.z = world.boardSide == 'W' ? 0 : (float)180;
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, myRote, 0.01f);
            transform.eulerAngles = myRote;
        }
    }
}
