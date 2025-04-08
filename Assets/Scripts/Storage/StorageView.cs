using UnityEngine;
using TMPro;

public class StorageView : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    [SerializeField] private TMP_Text _textLable;

    private void OnEnable()
    {
        _storage.ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _textLable.text = value.ToString();
    }
}