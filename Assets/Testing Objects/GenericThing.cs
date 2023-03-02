using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericThing : MonoBehaviour
{
    public virtual void doSomething(int x, int y) {
        Debug.Log(x + y);
    }
    
}
