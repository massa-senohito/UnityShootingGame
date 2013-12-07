using UnityEngine;
using System.Collections;
public class SoiBullet : MonoBehaviour
{
    public Vector3 bulDirect;
    public Shooter shooter;
    void Awake()
    {
        if (shooter == Shooter.Enemy)
        {
            //bulDirect = Vector3.forward;
        }
        else
        {
            //bulDirect = Vector3.back;
            //transform.rotation = Quaternion.Euler(Xform.YTo0(transform.rotation.eulerAngles));
        }
        Destroy(gameObject, 2);
    }
    // Use this for initialization
    void Start()
    {
        //renderer.material.SetColor("", new Color(200, 12, 12));
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {

        rigidbody.AddRelativeForce(bulDirect, ForceMode.Impulse);
    }
}

