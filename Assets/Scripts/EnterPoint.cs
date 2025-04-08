using UnityEngine;

public class EnterPoint : MonoBehaviour
{
    [SerializeField] private ResourceGenerator _resourceGenerstor;
    [SerializeField] private Base _base;

    private void Start()
    {
        _resourceGenerstor.Generate();
        _base.SpawnUnits();
    }
}