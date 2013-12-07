using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

  enum CollideObjs
  {
    None=0,
    Enemy,
    Bullet,
    Build=4,
  }
  enum Constrain
  {
    None,
    Left,
    Right,
    Missile,//�܂��X���X�^�[����ꂽ���ɕK�v����
    All
  }
  class PlayerBehav : MonoBehaviour
  {
#pragma warning disable 649
    public GameObject bullet;
    public Constrain notmove;
    public GameObject targetBox;
#pragma warning restore 649
    TargetBox t;
    public Vector3 left = Vector3.right * 1.2f;
    public Vector3 right = Vector3.left * 1.2f;
    Quaternion origin;
    //�N�I�[�^�j�I����+���Z���ł��Ȃ�,*���Z�ŕ\����i�œK��

    bool shottable = true;
    void Start()
    {
      var v = Vector3.zero.addX(2);
      print(v);
      origin = Quaternion.Euler(new Vector3(0, 180, 0));
      t = targetBox.GetComponent<TargetBox>();
    }
    Quaternion return2Origin(Quaternion q)
    {
      transform.position = Xform.ZToN(transform.position, -24);
      return Quaternion.RotateTowards(q, origin, 1f);
    }
    void RotateZ(float angle)
    {
      rigidbody.AddTorque(new Vector3(0, 0, angle), ForceMode.Force);
    }
    IEnumerator shotBul(float waittime)
    {
      shottable = false;
      yield return new WaitForSeconds(waittime);
      shottable = true;
    }
    //todo x���ƃ��b�N�I���ΏۑS���ɑł�
    //���̂܂ɂ����b�N���S�ĊO��Ă���̂ɐV�������b�N�I������Ȃ��Ȃ�?
    void ShotBullet(bool horm)
    {
      var bul = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
      Physics.IgnoreCollision(collider, bul.collider);
      var b = bul.GetComponent<Bullet>();
      b.Toward(targetBox.transform.position);
      if (horm) lockon(b);
    }
    void lockon(Bullet b)
    {
      var enemies = StageObserver.Enemies;
      if (enemies.Count() != 0)
      {
        var shotToward = enemies.FirstOrDefault(i => { return i.Locked && !i.Shooted; });
        //�܂�������Ă��Ȃ��ŏ��̓G��T����,���Ȃ���΂��̂܂܁A����΃��b�N����
        if (shotToward == null) return;
        shotToward.Shooted = true;
        b.Lock(shotToward);
      }
    }
    float stopPos = -26;
    void Update()
    {
      if (transform.position.z > stopPos) particleSystem.enableEmission = false;
      if (notmove == Constrain.All) return;
      transform.rotation = return2Origin(transform.rotation);
      if (Hom && shottable)
      {
        StartCoroutine(shotBul(0.5f));
        ShotBullet(true);
      }
      if (Fire)
      {
        ShotBullet(false);
      }
      t.CopyLocate(transform.position.x);
    }
    public int maxMoveWidth = 40;
    void FixedUpdate()
    {
      t.LockOn(transform.position);
      rigidbody.angularVelocity /= 1.1f; rigidbody.velocity /= 1.05f;
      if (LeftMove && maxMoveWidth > transform.position.x)
      {
        rigidbody.AddForce(left, ForceMode.Impulse); RotateZ(-10);
      }
      if (RightMove && -maxMoveWidth < transform.position.x)
      { rigidbody.AddForce(right, ForceMode.Impulse); RotateZ(10); }
      transform.position = Xform.YTo0(transform.position);
    }
    void GameOver()
    {
      //BroadcastMessage("GameOver", gameObject); �������d���̂�StackOverFlow���Ă�
      //notmove = Constrain.All;
      StageObserver.GameOver = true;
    }
    void OnTriggerEnter(Collider collision)
    {
      switch (Utils.collideWith(collision.gameObject))
      {
        case CollideObjs.Enemy: GameOver(); break;
        case CollideObjs.Build: GameOver(); break;
        case CollideObjs.Bullet: GameOver(); break;
        default: break;
      }
    }
    void OnCollisionEnter(Collision collision)
    {
      OnTriggerEnter(collision.collider);
    }


    bool LeftMove
    {
      get { 
          return Input.GetKey(KeyCode.LeftArrow); 
	  
      }
    }
    bool RightMove
    {
      get { return Input.GetKey(KeyCode.RightArrow); }
    }
    bool Fire
    {
      get { return Input.GetKeyDown(KeyCode.Z); }
    }
    bool Hom
    {
      get { return Input.GetKeyDown(KeyCode.X); }
    }
  }
