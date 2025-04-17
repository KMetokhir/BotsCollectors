using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagInstaller : MonoBehaviour
{
    private Flag _flag;

    public void ProcessMouseTap(RaycastHit hitData)
    {
        if(hitData.collider.TryGetComponent(out IFlagHolder flagHolder))
        {
            Flag flag = flagHolder.GetFlag();
            SetFlag(flag);

            return;
        }
        
        if(hitData.collider.TryGetComponent(out Earth earth) && _flag != null)
        {
            _flag.Install(hitData.point);
        }
    }

    private void SetFlag(Flag flag)
    {
        if (flag == null)
        {
            return;
        }

        if (flag == _flag)
        {
            flag.Uninstall();
        }
        else
        {            
            _flag = flag;            
        }
    }    
}
