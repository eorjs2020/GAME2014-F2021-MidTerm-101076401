using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationCheck : MonoBehaviour
{
    
    public Camera cam;
    // Start is called before the first frame update    
    // Update is called once per frame
    void Update()
    {
        // Change the EulerAngle about all game object by Landspace or Portrait.
        // Also Change orthsize because Landscape and portrait mode ratio is diff.
        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
                cam.orthographicSize = 3;                
                break;
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                cam.orthographicSize = 6;
                break;

        }
    }
}
