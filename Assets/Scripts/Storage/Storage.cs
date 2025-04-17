using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public event Action<int> ValueChanged;
    public uint Value { get; private set; }

    private void Awake()
    {
        ValueChanged?.Invoke((int)Value);
    }

    public void AddResource()
    {
        Value++;
        ValueChanged?.Invoke((int)Value);
    }

    public void Reduce(uint value)
    {
        if (Value < value)
        {
            throw new InvalidOperationException($"Storage value {Value} < decrease value {value}");
        }
    }
}