using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsData : MonoBehaviour
{
    private List<Unit> _units;

    private void Awake()
    {
        _units = new List<Unit>();
    }

    public void Add(Unit unit)
    {
        if (_units.Contains(unit))
        {
            throw new System.Exception("UnitsBase already contains unit");
        }

        _units.Add(unit);
    }

    public bool Contains(Unit unit)
    {
        return _units.Contains(unit);
    }

    public bool TryGetAllAvalibleUnits(out IEnumerable<Unit> avalibleUnits)
    {
        avalibleUnits = _units.Where(unit => unit.IsAvalible == true);

        bool unitsFound = avalibleUnits.Count() > 0;

        return unitsFound;
    }
}
