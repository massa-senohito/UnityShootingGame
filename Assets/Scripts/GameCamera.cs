using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class GameCamera : MonoBehaviour
{
    public GameObject target;


    public Vector3 stretch
    {
        get;
        set;
    }

    public Vector3 force
    {
        get;
        set;
    }
    public Vector3 acceleration
    {
        get;
        set;
    }
    public Vector3 velocity
    {
        get;
        set;
    }

    public float stiffness;

    public float damping;

    public float mass;
    public static GameCamera mainCam;

    public Vector3 MovetoPlayer()
    {
        stretch = transform.position - target.transform.position;
        force = -stiffness * stretch - damping * velocity;
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        transform.position += Xform.PosIgnoreYZ(velocity * Time.deltaTime);
        return transform.position;
    }

    // Use this for initialization
    void Start(){
      damping = 2 * Mathf.Sqrt((stiffness * mass));
      mainCam = this;
      //marks.ForEach(g=>g=renderer.guiTexture);
    }
    void FPSCamera()
    {
        //transform.LookAt(TargetBox.Position);
    }
    
    void Update(){
      
      MovetoPlayer();
      //TargetBox.LockedEnemy.Select<Vector3,Vector3>(mainCam.WorldToScreenPoint).ForEach(DrawLockSphere);
    }
}
