using UnityEngine;
using System.Collections;

public static class Xform{


    public static Quaternion Toword(MonoBehaviour from, MonoBehaviour to)
    {
        return Quaternion.LookRotation(Xform.DifPosition(from, to));
    }
    public static void Raise(MonoBehaviour m, float f)
    {
        m.transform.position += new Vector3(0, f, 0);
    }

    public static Quaternion ToBlenderAng(Vector3 ang)
    {
        float x = ang.x;
        if (x > 270) { x /= 1.1f; }else { x *= 1.1f; }
        return Quaternion.Euler(new Vector3(x, ang.y, ang.z));
    }
    public static Vector3 RightsideUnitSphere
    {
        get
        {
            return new Vector3(Mathf.Abs(Random.value), Random.value, Random.value);
        }
    }
    public static Quaternion BlenderRot
    {
        get { return Quaternion.Euler(new Vector3(270, 0, 0)); }
    }
    public static float Distance(GameObject g, GameObject gg)
    {
        return Vector3.Distance(g.transform.position, gg.transform.position);
    }
    //è≠ÇµèÍà·Ç¢
    public static Vector3 Velocity(MonoBehaviour m) { return m.rigidbody.velocity; }
    public static Vector3 PosIgnoreZ(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y, 0);
    }
    
    public static Vector3 PosIgnoreYZ(Vector3 pos)
    {
        return new Vector3(pos.x, 0, 0);
    }
    public static Vector3 ConstraintY(Vector3 pos, float miny,float maxy)
    {
        var t = new Vector3(pos.x, Mathf.Clamp(pos.y, miny, maxy), pos.z); 
        return t;
    }
    public static Vector3 ConstraintX()
    {
        return new Vector3(); 
    }
    public static Vector3 YTo0(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y / 1.1f, pos.z);
    }
    public static Vector3 DifPosition(MonoBehaviour e, MonoBehaviour ee)
    {
        return e.transform.position - ee.transform.position;
    }
    
    public static Vector3 ZTo0(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y, pos.z / 1.1f);
    }
    public static Vector3 ZToN(Vector3 pos,float n)
    {
        return new Vector3(pos.x, pos.y, pos.z / 1.1f+n/10);
    }
    public static Vector3 ZYTo0(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y/1.1f, pos.z / 1.1f);
    }
    public static Quaternion RotLeft
    {
        get
        {
            return Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }
    public static Quaternion RotRight
    {
        get
        {
            return Quaternion.Euler(new Vector3(0, -90, 0));
        }
    }
}
