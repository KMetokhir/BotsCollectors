using System;

public interface UnitEvents
{
    public event Action<Unit> BecameAvailable;
    public event Action<Unit> ResourceCollected;
    public event Action<Unit, Base> NewBaseBuild;
}