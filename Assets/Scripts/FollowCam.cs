using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour
{
    static public FollowCam S; // a follow cam singleton
    //fields set in Unity inspector pane
    public float easing = 0.01f;
    public Vector2 minXY;
    public bool ______________________;

    //fields set dynamically
    public GameObject poi; //The point of interest
    public float camZ; //The desired Z pos of the camera


    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        Vector3 destination;
        // If there is no poi, return to P:[0,0,0]
        // if there's only one line following an if, it doesn't need braces
        if (poi == null)
        {
            destination = Vector3.zero; //return if there is no point
        } else
        {
            //Get the position of the poi
            destination = poi.transform.position;
            // If poi is a projectile, check to see if its at rest
            if (poi.tag == "Projectile")
            {
                //If it is sleeping (not moving)
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    // return to default view
                    poi = null;
                    // in the next update
                    return;
                }
            }
        }

        //Get the position of the poi
        //Vector3 destination = poi.transform.position; OLD Code
        //Limit the X & Y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //Retain a destination.z of camZ
        destination.z = camZ;
        //Set the camera to the destination
        transform.position = destination;
        //Set the orthographicSIze of the Camera to keep Ground in view
        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
	}
}
