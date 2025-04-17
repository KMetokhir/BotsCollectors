using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UnitsData : MonoBehaviour
{
    private List<Unit> _units;

    public IReadOnlyList<UnitEvents> UnitsEvents { get; private set; }

    private void Awake()
    {
        _units = new List<Unit>();
        UnitsEvents = new List<Unit>(_units);
    }

    public void Add(Unit unit)
    {
        if (_units.Contains(unit))
        {
            throw new System.Exception("UnitsBase already contains unit");
        }

        _units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        if(Contains(unit)== false)
        {
            throw new InvalidOperationException("Doesn't contain unit");
        }

        _units.Remove(unit);
    }

    public bool Contains(Unit unit)
    {
        return _units.Contains(unit);
    }

    public bool TryGetAvalibleUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsAvalible == true);

        bool unitFound = unit != null;

        return unitFound;
    }

    public bool TryGetAllAvalibleUnits(out IEnumerable<Unit> avalibleUnits)
    {
        avalibleUnits = _units.Where(unit => unit.IsAvalible == true);

        bool unitsFound = avalibleUnits.Count() > 0;

        return unitsFound;
    }
}
