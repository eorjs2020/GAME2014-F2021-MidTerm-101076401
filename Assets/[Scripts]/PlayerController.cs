/*
 *Full Name        : Daekoen Lee 
 *Student ID       : 101076401
 *Date Modified    : October 24, 2021
 *File             : PlayerController.cs
 *Description      : This is Controller Script - it controls the Player moving
 *
 *Revision History : Changed player position and moving to work in Both Orientation
 */

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;

    [Header("Boundary Check")]
    public float horizontalBoundary;

    [Header("Player Speed")]
    public float horizontalSpeed;
    public float maxSpeed;
    public float horizontalTValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;

    private ScreenOrientation orientation;
    // Start is called before the first frame update
    void Start()
    {
        orientation = Screen.orientation;
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
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
        _FireBullet();
    }

     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 30 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position);
        }
    }

    private void _Move()
    {
        // Moving Change by Screen.orientation
        float direction = 0.0f;
        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                foreach (var touch in Input.touches)
                {
                    var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

                    if (worldTouch.y > transform.position.y)
                    {
                        // direction is positive
                        direction = 1.0f;
                    }

                    if (worldTouch.y < transform.position.y)
                    {
                        // direction is negative
                        direction = -1.0f;
                    }

                    m_touchesEnded = worldTouch;

                }

                // keyboard support
                if (Input.GetAxis("Vertical") >= 0.1f)
                {
                    // direction is positive
                    direction = 1.0f;
                }

                if (Input.GetAxis("Vertical") <= -0.1f)
                {
                    // direction is negative
                    direction = -1.0f;
                }

                if (m_touchesEnded.y != 0.0f)
                {
                    transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, m_touchesEnded.y, horizontalTValue));
                }
                else
                {
                    Vector2 newVelocity = m_rigidBody.velocity + new Vector2(0.0f, direction * horizontalSpeed);
                    m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
                    m_rigidBody.velocity *= 0.99f;
                }                
                break;
            case ScreenOrientation.Portrait:
                foreach (var touch in Input.touches)
                {
                    var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

                    if (worldTouch.x > transform.position.x)
                    {
                        // direction is positive
                        direction = 1.0f;
                    }

                    if (worldTouch.x < transform.position.x)
                    {
                        // direction is negative
                        direction = -1.0f;
                    }

                    m_touchesEnded = worldTouch;

                }

                // keyboard support
                if (Input.GetAxis("Horizontal") >= 0.1f)
                {
                    // direction is positive
                    direction = 1.0f;
                }

                if (Input.GetAxis("Horizontal") <= -0.1f)
                {
                    // direction is negative
                    direction = -1.0f;
                }

                if (m_touchesEnded.x != 0.0f)
                {
                    transform.position = new Vector2(Mathf.Lerp(transform.position.x, m_touchesEnded.x, horizontalTValue), transform.position.y);
                }
                else
                {
                    Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
                    m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
                    m_rigidBody.velocity *= 0.99f;
                }
                break;        

        }
        // touch input support
        
    }

    private void _CheckBounds()
    {
        //CheckBounds Change by Screen.orientation
        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                // check right bounds
                if (transform.position.y >= horizontalBoundary)
                {
                    transform.position = new Vector3(transform.position.x, horizontalBoundary, 0.0f);
                }

                // check left bounds
                if (transform.position.y <= -horizontalBoundary)
                {
                    transform.position = new Vector3(transform.position.x, -horizontalBoundary, 0.0f);
                }
                break;
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                // check right bounds
                if (transform.position.x >= horizontalBoundary)
                {
                    transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
                }

                // check left bounds
                if (transform.position.x <= -horizontalBoundary)
                {
                    transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
                }
                break;
        }
        

    }
}
