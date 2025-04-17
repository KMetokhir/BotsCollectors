using UnityEngine;

public class EnterPoint : MonoBehaviour
{
    [SerializeField] private ResourceGenerator _resourceGenerstor;
    [SerializeField] private UnitGenerator _unitGenerator;
    [SerializeField] private int _unitsCount;

    private void Start()
    {
        _resourceGenerstor.Generate();

        for (int i = 0; i < _unitsCount; i++)
        {
            _unitGenerator.TryGenerateUnit();
        }
    }
}