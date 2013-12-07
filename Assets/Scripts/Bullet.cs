
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//レイ
//scene
//数フレームの間あたり判定をなくす
public enum Shooter
{
    Player,Enemy,
}
class Bullet : MonoBehaviour
{
    public Vector3 bulDirect;
    public Enemy target;
    SpringMove spring2enemy;
    int per;
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
        Destroy(gameObject, 3);
    }
    public void Toward(Vector3 pos)
    {
        transform.LookAt(pos);
    }
    public void Lock(Enemy e)
    {//playerbehav中から呼ばれる,呼ばれた後Target方向に移動するようここで実装する
      target = e;
    }
    void Start()
    {
	    //horming
      spring2enemy = new SpringMove(1.2f,0.3f,3.5f);
	  collider.enabled=false;
    }
    void Update()
    {
    }
    void OnCollisionEnter(Collision collision) 
    {
        Destroy(gameObject);
    }
    //public void toWard(Vector3 pos)
    //{
    //    transform.rotation = Quaternion.FromToRotation(transform.position, pos);
    //}
    void FixedUpdate()
    {
      if (per > 20) collider.enabled = true;//プレイヤーとの衝突を防ぐ
      if (target == null) { rigidbody.AddRelativeForce(bulDirect, ForceMode.Impulse); }
      else { //todo 敵と自分の結んだ直線の後ろ側にターゲットを移動させることで、ばねを強くする
        if(per<40)transform.position = spring2enemy.Moveto(transform.position, target);
        rigidbody.AddForce(Xform.Toword(this,target).eulerAngles.normalized*2);
      }
      per++;
    }
    public float accel;
}
