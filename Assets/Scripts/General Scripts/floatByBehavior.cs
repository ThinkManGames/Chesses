using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatByBehavior : MonoBehaviour
{
    private float timer = 0.0f;
    private float maxTime;
    private bool moving = false;
    private Camera mainCamera = null;
    private float startingY;
    private float endingY;
    private Vector3 ending;
    private float roteSpeed;
    private randParent parent = null;

    // Start is called before the first frame update
    void Start()
    {
        parent = FindObjectOfType<randParent>();
        mainCamera = FindObjectOfType<Camera>();
        startingY = mainCamera.transform.position.y + mainCamera.orthographicSize + 2;
        endingY = mainCamera.transform.position.y - mainCamera.orthographicSize - 5;
        setMaxTime();
        setPos();
        setRoteSpeed();
        setEnding();
    }
    private void setRoteSpeed()
    {
        roteSpeed = parent.rand.Next(1, 5);

    }
    private void setMaxTime()
    {
        maxTime = parent.rand.Next(0, 5);
    }

    private Vector3 setPos()
    {
        int startingX = parent.rand.Next(-10, 11);
        return new Vector3(startingX, startingY, 0);
    }

    private void setEnding()
    {
        int endingX = parent.rand.Next(-10, 11);
        ending = new Vector3(endingX, endingY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, ending,0.005f);
            transform.Rotate(new Vector3(0, 0, roteSpeed));
            if(Mathf.Abs(transform.position.x - ending.x) < 0.05)
            {
                moving = false;
                setMaxTime();
                transform.position = setPos();
            }
        } else
        {
            timer += Time.deltaTime;
            if (timer > maxTime)
            {
                timer -= maxTime;
                moving = true;
                setEnding();
            }
        }
    }
}
