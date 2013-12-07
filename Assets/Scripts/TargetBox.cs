using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class LockedInfo
{
    public Enemy Locked
    {
        get;
        private set;
    }
    public bool AlreadyShooted
    {
        get;
        set;
    }
    public LockedInfo(Enemy e)
    {
        Locked = e;
    }
    public Vector3 Position
    {
        get { return Locked.transform.position; }
    }
    public bool Destroyed
    {
    //ここで関連付けられたマーカーを消すべきかも
        get { if (!Locked) return true; else return false; }
    }
    public override int GetHashCode(){
      return (int)Position.z+(AlreadyShooted?1:0);
    }
    public override bool Equals(object other){
      //マーカー同士の比較のみ対応、マーカーと敵だとtrueになる可能性も
      var go=other as GameObject;
      if(go!=null && (go.GetHashCode()==GetHashCode()))return true;
      return false;
    }
    public override string ToString(){
      var s="shoted";
      s+=AlreadyShooted ? "already":" not";
      return s + Position;
    }
}
public class TargetBox : MonoBehaviour{
  public int EnemyUnlockCount=5*60;
  //public Vector3 LockedPos;
  public int lockedEnemies;
  // Use this for initialization
  void Start()
  {
  }
  public float rate = 100;
  //public static List<LockedInfo> LockedEnemy=new List<LockedInfo>();
  public bool[] already; 
  // Update is called once per frame
  void Update(){

  }
  public void LockOn(Vector3 playerPos){
    //クロージャからgameobject呼べないらしい
    Physics.RaycastAll(playerPos, transform.position).ForEach(
      h =>
      {
        //Listに入れられた敵はn* Time.deltaTime後にリストから削除
        var e = h.transform.GetComponent<Enemy>();
        if (e == null) return;if (e.Locked) return;
        e.Locked = true;
        //var i=new LockedInfo(e);
        Marker.SetLock(e);
        timer = EnemyUnlockCount;
      });
  }
  public int timer = 1;
  void LockEnemyManage()
  {
      var enemies = StageObserver.Enemies;
      if (enemies.Count == 0) return;

      lockedEnemies = enemies.Count(e => e.Locked);
      already = enemies.Select(x => x.Shooted).ToArray();
      //ロックオンされている敵が１体でもいるとタイマーは減り
      if (enemies.Count(e => e.Locked) != 0) { timer--; } else { timer = EnemyUnlockCount; }
      if (timer == 0) { Marker.UnLock(enemies[0]); enemies.RemoveAt(0); timer = EnemyUnlockCount; }
      StageObserver.Enemies.Where
       (i => { if (!i) { Marker.UnLock(i); } return i; }).ToList();
  }
  void FixedUpdate()
  {
    LockEnemyManage();

    if (TopMove) { transform.position += Vector3.up * rate / 100; }
    else
    {
      if (DownMove) { transform.position += Vector3.down * rate / 100; }
      else { transform.position = Xform.YTo0(transform.position); }
    }
    transform.position = Xform.ConstraintY(transform.position, -15, 15);
  }
  public void CopyLocate(float x)
  {
    transform.position = new Vector3(x, transform.position.y, transform.position.z);
  }
  bool DownMove
  {
    get
    {
      return Input.GetKey(KeyCode.DownArrow);
    }
  }
  bool TopMove
  {
    get { return Input.GetKey(KeyCode.UpArrow); }
  }
}
