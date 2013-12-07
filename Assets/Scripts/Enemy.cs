using UnityEngine;
using System.Collections;
using System.Linq;


public class EnemyDeadEventArgs : System.EventArgs
{
  public Enemy enemy;
  public bool HitAny;
  public EnemyDeadEventArgs(Enemy e, bool hitAny)
  {
    enemy = e;
    HitAny = hitAny;
  }

}
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ParticleSystem))]
public class Enemy : MonoBehaviour, IEqualityComparer
{
  public static event System.EventHandler<EnemyDeadEventArgs> OnDead;
  public GameObject enbullet;
  public Vector3 BrowDirection = Vector3.zero;
  public Transform first;
  public Transform second;
  public EnemyAI AI = EnemyAI.FollowPath;
  public bool Dieing;
  public GameObject player;
  Transform startP;
  public Transform[] points { get; private set; }
  public bool Locked = false;
  public bool Shooted = false;
  // Use this for initialization
  void Awake()
  {
    particleSystem.enableEmission = false;
    iTween.Init(gameObject);
    var pos = transform.position;
    startP = transform;
    //左にいるほう、右にいるほうでパスを逆にする
    if (pos.x > 0 && AI == EnemyAI.FollowPath)
    {
      //first = GameObject.FindGameObjectWithTag("rwayo").transform;
      second = GameObject.FindGameObjectWithTag("rwayi").transform;
    }
    points = new Transform[] { startP, first, second };
  }
  void Start()
  {
    //animation.enabled = false;//アニメーションで位置を上書きしてた
  }
  int _count = 0;
  int Count
  {
    get { _count++; return _count; }
  }
  bool Attacking
  {
    get
    {
      if (Count % 80 == 0 && Xform.Distance(player, gameObject) < 60f) return true;
      return false;
    }

  }
  GameObject scopePrinter;
  bool hitAny;
  void BreakIn(float i)
  {
    //Instantiate(scopePrinter);//todo スコアを表示する
    particleSystem.enableEmission = true;
    Dieing = true;//trueで当たったらポイントを2倍する
    OnDead(this, new EnemyDeadEventArgs(this,hitAny));
    var pos = transform.position;
    //判定に自分が含まれているから、自分ぬきでカウント
    var nearenemy = Physics.OverlapSphere(transform.position, 4)
        .ExceptMySelf(gameObject.collider).FirstOrDefault(c => c.tag == "enemy");
    //blowoffの方向へ飛んでいる
    if (nearenemy != null)
    {
      if (BrowDirection != Vector3.zero) { rigidbody.AddForce(BrowDirection); }
    }
    Destroy(gameObject, i);
  }
  void OnDestroy()
  {
  }
  void OnCollisionEnter(Collision collision)
  {
    rigidbody.velocity *= 3;
    hitAny = true;
    switch (Utils.collideWith(collision.gameObject))
    {
      case CollideObjs.Enemy: BreakIn(1); break;
      case CollideObjs.Build: BreakIn(1); break;
      case CollideObjs.Bullet: BreakIn(2); break;
      default: break;
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
  public float per;

  void FixedUpdate()
  {
    if (Attacking)
    {
      var bul = Instantiate(enbullet, transform.position, Quaternion.identity) as GameObject;
      Physics.IgnoreCollision(collider, bul.collider);
    }
    if (transform.position.z > -30) { BreakIn(1); }
    if (rigidbody.velocity.magnitude > 7) { rigidbody.velocity /= 1.1f; }
    EnemyUtil.nextToDo(this);
    //.velocity = Vector3.forward * 2;//操作しづらいので
    //mes = "coming";
    //mes = "stay";

  }

  public bool Equals(object x, object y)
  {
    var e = x as Enemy; var ee = y as Enemy;
    if (e == null || ee == null) return false;
    return e.transform.position == ee.transform.position;
  }

  public int GetHashCode(object obj)
  {
    return 2;
  }
}
