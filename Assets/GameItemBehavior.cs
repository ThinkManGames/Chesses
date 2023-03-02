using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameItemBehavior : MonoBehaviour
{
    private NetworkManager manager;
    public string gameID;
    public string hostName;
    public string gameType;

    [SerializeField] Text myHostName;
    [SerializeField] Text myGameType;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        myHostName.text = hostName;
        myGameType.text = gameType;
    }

    public void ImSelected()
    {
        manager.GameSelected(this);
    }
}
