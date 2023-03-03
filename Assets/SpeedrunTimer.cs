using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedrunTimer : MonoBehaviour
{
    private TheWorld world = null;
    public double currentTime = 0.0f;
    public bool counting = false;
    [SerializeField] Text text;
    public double maxTime = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        world = FindObjectOfType<TheWorld>();
    }

    public void StartTimer()
    {
        currentTime = maxTime;
        counting = true;
        ((SpeedrunCapture)world.capture).StartTimer();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (counting)
        {
            if (currentTime <= 0.0f)
            {
                currentTime = 0.0f;
                ((SpeedrunCapture)world.capture).TimeUp();
                world.possibleSpots[0,0] = 1;
                SpotBehavior spot = new SpotBehavior();
                spot.row = 0;
                spot.col = 0;
                string[] oldMoves = new string[2];
                oldMoves[1] = world.showOldMove[1];
                oldMoves[0] = world.showOldMove[0];
                world.selectSpot(spot);
                world.showOldMove = oldMoves;
                counting = false;
                text.text = currentTime.ToString();
            }
            else
            {
                currentTime -= Time.deltaTime;
                text.text = currentTime.ToString();
            }
        }
    }
}
