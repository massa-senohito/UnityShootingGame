using UnityEngine;
public class SpringMove  
{
    //[SerializeField]
    //[Serializable]?
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
    //	{
    //		get; set;
    //	}
    public float damping;
    //	{
    //		get; set;
    //	}
    public float mass;
    //	{
    //		get; set;
    //	}

    public SpringMove(float mass,float damping,float stiffness)
    {
        this.mass = mass;
        this.damping = damping;
        this.stiffness = stiffness;
    }
    public void MakeDampingBest()
    {
        damping = 2 * Mathf.Sqrt((stiffness * mass));
    }
    public Vector3 Moveto(Vector3 m, MonoBehaviour mm) { return Moveto(m, mm.transform.position); }
    public Vector3 Moveto(Vector3 nowPosition, Vector3 target)
    {
        stretch = nowPosition - target;
        force = -stiffness * stretch - damping * velocity;
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        nowPosition += velocity * Time.deltaTime;
        return nowPosition;
    }
}