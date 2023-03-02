using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Camera mainCamera = null;
    public TheWorld theWorld = null;

    public LayerMask spotMask;
    // Start is called before the first frame update
    void Start()
    {
        theWorld = FindObjectOfType<TheWorld>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            if (Physics2D.Raycast(mousePos2D,Vector2.zero,1000,spotMask))
            {
                theWorld.selectSpot(Physics2D.Raycast(mousePos2D, Vector2.zero, 1000, spotMask).collider.gameObject.GetComponent<SpotBehavior>());
            }
        }
    }
}
