using UnityEngine;
using System;
using System.Collections;

public class UIDelegate
{
    public delegate void Update (bool result);
    public delegate IEnumerator Load (WWW bundle);
}

