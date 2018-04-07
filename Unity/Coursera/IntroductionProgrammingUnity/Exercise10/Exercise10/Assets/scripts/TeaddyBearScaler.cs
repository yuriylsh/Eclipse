using System;
using Assets.scripts;
using UnityEngine;

public class TeaddyBearScaler: MonoBehaviour {
    private void Start ()
    {
       if(gameObject.name.EndsWith("yellow", StringComparison.OrdinalIgnoreCase))
       {
           transform.Scale(4f, 4f);
       }
       if(gameObject.name.EndsWith("green", StringComparison.OrdinalIgnoreCase))
       {
           transform.Scale(y:3f);
       }
       if(gameObject.name.EndsWith("purple", StringComparison.OrdinalIgnoreCase))
       {
           transform.Scale(3f);
       }
	}
}
