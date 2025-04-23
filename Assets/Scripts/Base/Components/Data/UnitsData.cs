using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsData : MonoBehaviour
{
    private List<Unit> _units;

    public IReadOnlyList<Unit> Units { get; private set; }
    public int Count => _units.Count;
    public int AvalibleUnitsCount => _units.Where(unit => unit.IsAvalible == true).Count();

    private void Awake()
    {
        _units = new List<Unit>();
        Units = new List<Unit>(_units);
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
        if (Contains(unit) == false)
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

        return unit != null;
    }

    public bool TryGetAllAvalibleUnits(out IEnumerable<Unit> avalibleUnits)
    {
        avalibleUnits = _units.Where(unit => unit.IsAvalible == true);

        return avalibleUnits.Count() > 0;
    }
}