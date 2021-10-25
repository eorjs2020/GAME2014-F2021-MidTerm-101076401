/*
 *Full Name        : Daekoen Lee 
 *Student ID       : 101076401
 *Date Modified    : October 24, 2021
 *File             : BulletManager.cs
 *Description      : This is Manager Script - it manage the bullet pool.
 *
 *Revision History : Changed Bullet angle to work in Both Orientation
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    public BulletFactory bulletFactory;
    public int MaxBullets;

    private Queue<GameObject> m_bulletPool;


    // Start is called before the first frame update
    void Start()
    {
        _BuildBulletPool();
    }

    private void _BuildBulletPool()
    {
        // create empty Queue structure
        m_bulletPool = new Queue<GameObject>();

        for (int count = 0; count < MaxBullets; count++)
        {
            var tempBullet = bulletFactory.createBullet();
            tempBullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
            m_bulletPool.Enqueue(tempBullet);
        }
    }

    public GameObject GetBullet(Vector3 position)
    {
        var newBullet = m_bulletPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        // Bullet Angle change by Screen.orientation
        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:                   
            case ScreenOrientation.LandscapeRight:
                newBullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
                break;
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                newBullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                break;
        }
        return newBullet;
    }

    public bool HasBullets()
    {
        return m_bulletPool.Count > 0;
    }

    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        m_bulletPool.Enqueue(returnedBullet);
    }
}
