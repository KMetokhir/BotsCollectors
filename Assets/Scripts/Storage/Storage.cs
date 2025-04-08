using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private uint value;

    public event Action<int> ValueChanged;

    private void Awake()
    {
        ValueChanged?.Invoke((int)value);
    }

    public void AddResource()
    {
        value++;
        ValueChanged?.Invoke((int)value);
    }
}