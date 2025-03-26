using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFolower : MonoBehaviour
{
    [SerializeField]private Transform _folower;

    private float _screenWidth = Screen.width;
    private float _screenHeight = Screen.height;
    

    private void Update()
    {        
        if(IsMouseInBounds(_screenWidth, _screenHeight)== false)
        {
            Debug.Log("outOfBounds");
        }                
    }




    private bool IsMouseInBounds(float x, float y)
    {
        bool inBounds = false;
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        if (mousePosition.x > 0 && mousePosition.x < x)
        {

            if (mousePosition.y > 0 && mousePosition.y < y)
            {
                inBounds = true;

            }
            else
            {
                inBounds = false;
            }
        }
        else
        {
            inBounds = false;
        }

        return inBounds;
    }
}