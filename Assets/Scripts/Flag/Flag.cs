using System;
using UnityEngine;

public class Flag : MonoBehaviour, IUnitTarget
{
    [SerializeField] Transform Transform;
    
    public event Action<Flag> Installed;
    public event Action Uninstalled;
    public Vector3 Position => transform.position;

    private void Awake()
    {
        Uninstall();
    }

    public void Install(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;

        Installed?.Invoke(this);
    }

    public void Uninstall()
    {
        Uninstalled?.Invoke();
        gameObject.SetActive(false);
    }  
}