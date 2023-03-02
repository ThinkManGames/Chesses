using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingLoader : MonoBehaviour
{
    
    public GenericThing thing = null;
    // Start is called before the first frame update
    void Start()
    {
        thing.doSomething(20, 10);
    }

}
