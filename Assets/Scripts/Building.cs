using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    public int speed=49;
    public float turn=0.19f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
	}
    void FixedUpdate()
    {//todo �Ȃ����Ă���悤�Ɍ�����ɂ͉����牡�̏�Ԃł܂���������Ɍ������悤�ɂ���
        float sp = speed / 100.0f;
        rigidbody.AddTorque(Vector3.up * turn);
        rigidbody.AddRelativeForce(Vector3.forward*sp, ForceMode.Impulse);

    }
}
