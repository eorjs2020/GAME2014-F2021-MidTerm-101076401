/*
 *Full Name        : Daekoen Lee 
 *Student ID       : 101076401
 *Date Modified    : October 24, 2021
 *File             : BackgroundController.cs
 *Description      : This is Controller Script - it controls the motion of Scrolling Backround
 *
 *Revision History : Changed Backgournd to work in Both Orientation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float horizontalSpeed;
    public float horizontalBoundary;
    private ScreenOrientation orientation;
    private Vector3 originalP;
    // Update is called once per frame
    private void Start()
    {
        orientation = Screen.orientation;        
    }
    void Update()
    {
        // check Orientation and if there is change orientation change position to Right Position
        if (orientation != Screen.orientation)
        {
            switch(orientation)
            {
                case ScreenOrientation.LandscapeLeft:                     
                case ScreenOrientation.LandscapeRight:
                    if (orientation == ScreenOrientation.Portrait ||
                        orientation == ScreenOrientation.PortraitUpsideDown)
                    {
                        Vector2 temp1 = transform.position;
                        transform.position = new Vector3(temp1.y, 0);
                        
                    }
                    orientation = Screen.orientation;
                    break;
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    if (orientation == ScreenOrientation.LandscapeLeft 
                        || orientation == ScreenOrientation.LandscapeRight)
                    {
                        Vector2 temp2 = transform.position;
                        transform.position = new Vector3(0, temp2.x);
                        
                    }
                    
                    orientation = Screen.orientation;
                    break;
            }
        }
        _Move();
        _CheckBounds();
    }

    private void _Reset()
    {        
        switch(Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:                
            case ScreenOrientation.LandscapeRight:
                transform.position = new Vector3(horizontalBoundary, 0.0f);
                break;
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                transform.position = new Vector3(0.0f, horizontalBoundary);
                break;
        }        
    }

    private void _Move()
    {
        // Moving Change by Screen.orientation
        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:                
            case ScreenOrientation.LandscapeRight:                
                transform.position -= new Vector3(horizontalSpeed, 0.0f) * Time.deltaTime;
                break;
            case ScreenOrientation.Portrait:                
            case ScreenOrientation.PortraitUpsideDown:
                transform.position -= new Vector3(0.0f, horizontalSpeed) * Time.deltaTime;
                break;
        }
    }

    private void _CheckBounds()
    {
        //CheckBounds Change by Screen.orientation 
        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:                              
            case ScreenOrientation.LandscapeRight:
                // change y to x  
                if (transform.position.x <= -horizontalBoundary)
                {                    
                    _Reset();
                }
                break;
            case ScreenOrientation.Portrait:
                if (transform.position.y <= -horizontalBoundary)
                {
                    _Reset();
                }
                break;
            case ScreenOrientation.PortraitUpsideDown:
                if (transform.position.y <= -horizontalBoundary)
                {
                    _Reset();
                }
                break;

        }
        // if the background is lower than the bottom of the screen then reset
        
    }
}
