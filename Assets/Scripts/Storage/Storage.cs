using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private uint value;

    public event Action<int> ValueChanged;

    public void AddResource()
    {
        value++;
        ValueChanged?.Invoke((int)value);
    }
}
