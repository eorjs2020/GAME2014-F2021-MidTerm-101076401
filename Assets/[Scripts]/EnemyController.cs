/*
 *Full Name        : Daekoen Lee 
 *Student ID       : 101076401
 *Date Modified    : October 24, 2021
 *File             : EnemyController.cs
 *Description      : This is Controller Script - it controls the Enemy moving
 *
 *Revision History :
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float horizontalSpeed;
    public float horizontalBoundary;
    public float direction;
    private ScreenOrientation orientation;
    // Update is called once per frame
    private void Start()
    {
        orientation = Screen.orientation;
    }
    void Update()
    {
        if (orientation != Screen.orientation)
        {
            switch (orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    if (orientation == ScreenOrientation.Portrait)
                    {
                        Vector2 temp1 = transform.position;
                        transform.position = new Vector3(temp1.y, temp1.x);
                    }
                    orientation = Screen.orientation;
                    break;
                case ScreenOrientation.Portrait:
                    if (orientation == ScreenOrientation.LandscapeLeft
                        || orientation == ScreenOrientation.LandscapeRight)
                    {
                        Vector2 temp2 = transform.position;
                        transform.position = new Vector3(temp2.y, temp2.x);
                    }
                    orientation = Screen.orientation;
                    break;
            }
        }
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {        
        // Moving Change by Screen.orientation
        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                transform.position += new Vector3(0.0f, horizontalSpeed * direction * Time.deltaTime, 0.0f);
                break;
            case ScreenOrientation.Portrait:
                transform.position += new Vector3(horizontalSpeed * direction * Time.deltaTime, 0.0f, 0.0f);
                break;
        }
        
    }

    private void _CheckBounds()
    {
        // check Orientation and if there is change orientation change position to Right Position
        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                if (transform.position.y >= horizontalBoundary)
                {
                    direction = -1.0f;
                }

                // check down boundary
                if (transform.position.y <= -horizontalBoundary)
                {
                    direction = 1.0f;
                }
                break;
            case ScreenOrientation.Portrait:
                if (transform.position.x >= horizontalBoundary)
                {
                    direction = -1.0f;
                }

                // check down boundary
                if (transform.position.x <= -horizontalBoundary)
                {
                    direction = 1.0f;
                }
                break;
        }
        // check up boundary
        
    }
}
