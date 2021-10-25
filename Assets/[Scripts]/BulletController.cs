/*
 *Full Name        : Daekoen Lee 
 *Student ID       : 101076401
 *Date Modified    : October 24, 2021
 *File             : BulletController.cs
 *Description      : This is Controller Script - it controls the Bullet moving
 *
 *Revision History : Changed Bullet Moving to work in Both Orientation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IApplyDamage
{
    public float horizontalSpeed;
    public float horizontalBoundary;
    public BulletManager bulletManager;
    public int damage;
    private ScreenOrientation orientation;
    // Start is called before the first frame update
    void Start()
    {
        orientation = Screen.orientation;
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // check Orientation and if there is change orientation change position to Right Position
        if (orientation != Screen.orientation)
        {
            switch (orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    if (orientation == ScreenOrientation.Portrait ||
                        orientation == ScreenOrientation.PortraitUpsideDown)
                    {
                        Vector2 temp1 = transform.position;
                        transform.position = new Vector3(temp1.y, temp1.x);                        
                    }
                    orientation = Screen.orientation;
                    break;
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
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
                transform.position += new Vector3(horizontalSpeed, 0.0f, 0.0f) * Time.deltaTime;
                break;
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                transform.position += new Vector3(0.0f, horizontalSpeed, 0.0f) * Time.deltaTime;
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
                if (transform.position.x > horizontalBoundary)
                {
                    bulletManager.ReturnBullet(gameObject);
                }
                break;
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                if (transform.position.y > horizontalBoundary)
                {
                    bulletManager.ReturnBullet(gameObject);
                }
                break;
        }        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        bulletManager.ReturnBullet(gameObject);
    }

    public int ApplyDamage()
    {
        return damage;
    }
}
