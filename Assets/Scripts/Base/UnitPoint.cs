using UnityEngine;

public class UnitPoint : MonoBehaviour
{
    private Unit _unit;

    public void SetUnit(Unit unit)
    {
        if (_unit != null)
        {
            throw new System.Exception("Unit point is not empty, it is booked by " + _unit.ToString() + " Unit");
        }

        _unit = unit;
    }
    public bool TryGetPosition(out Vector3 position)
    {
        position = Vector3.zero;

        if (_unit == null)
        {
            position = transform.position;
        }

        return _unit == null;
    }

    public bool TryGetPosition(Unit unit, out Vector3 position)
    {
        bool isBookedByUnit = false;
        position = Vector3.zero;

        if (_unit != null)
        {
            if (_unit == unit)
            {
                position = transform.position;
                isBookedByUnit = true;
            }
        }

        return isBookedByUnit;
    }
}