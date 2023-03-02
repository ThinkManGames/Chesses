using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing2 : GenericThing
{
    public override void doSomething(int x, int y)
    {
        Debug.Log(x * y);
    }
}
