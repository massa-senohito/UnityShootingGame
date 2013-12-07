using UnityEngine;
using System.Linq;
using System.Collections.Generic;
class AndroidInput
{
    public static IEnumerable<Vector2> TuchedPos
    {
        get
        {
#if UNITY_ANDROID
            return Input.touches.Select(t => t.position);
#else
	    return Enumerable.Range(1,4).Select(t=>new Vector2(0,0));
#endif
        }
    }
    public static Vector3 direction
    {
        get { 
#if UNITY_ANDROID
	    //‚Á‚Ä•`‚¢‚Ä“à•”‚É‰B‚·‚×‚«
#endif
            return Input.gyro.attitude.eulerAngles; 
        }
    }

    Vector3 acceleration
    {
        get
        {
            Vector3 dir = Vector3.zero;
            dir.x = -Input.acceleration.y;
            dir.z = Input.acceleration.x;

            // clamp acceleration vector to the unit sphere
            if (dir.sqrMagnitude > 1) dir.Normalize();
            // Make it move 10 meters per second instead of 10 meters per frame...
            return dir * Time.deltaTime;
        }
    }
}
