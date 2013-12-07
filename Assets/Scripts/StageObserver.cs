using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
public class StageObserver : MonoBehaviour
{
  public GameObject building;
  public GameObject player;
  int BuildTiming = 100;
  public int flame;
  public GameObject enemy;
  public GameObject wallEnemy;
  public GameObject[] Markers = new GameObject[5];
  public static List<Enemy> Enemies = new List<Enemy>();
  Vector3 left = new Vector3(-7, 0, -60);
  Vector3 right = new Vector3(7, 0, -60);
  int Score;
  public static bool GameOver;
  bool Next
  {
    get { return Input.GetKeyDown(KeyCode.Z); }
  }

  // Use this for initialization
  void Start()
  {
    Utils.Iter2(Marker.Marks, Markers, (m, g) =>
    {
      var mark = g.GetComponent<Marker>(); m = mark;
    });
    for (var i = 0; i < 5; i++) { Marker.Marks[i] = Markers[i].GetComponent<Marker>(); };
    //iTween.CameraFadeAdd( RenderSettings.skybox.mainTexture;
    Enemy.OnDead += Enemy_OnDead;
    inst = instruct;
    guiText.text = "Zを押してください";

  }
  bool started;
  IEnumerator instruct
  {
    get
    {
      var main = Instructer.main;
      main.PrintInstruct(0);
      guiText.text = "Zで先に進みます";
      yield return null;
      main.PrintInstruct(1);
      yield return null;
      main.PrintInstruct(2);
      yield return null; 
      main.PrintInstruct(3);
      yield return null; 
      main.PrintInstruct(4);
      yield return null;
      main.PrintInstruct(5);
      guiText.text = "はじまります";
      yield return null;
      started = true;
      main.Clear();
      StartCoroutine(spown());
    }
  }

  //int followSc = 100,StraitSc=200;

  private void Enemy_OnDead(object sender, EnemyDeadEventArgs e)
  {
    //todo 倒された敵の撃墜表示をEnemyのちょっと上に表示>近くでいい気がする
    int multi = 1;
    Enemies.Remove(e.enemy);

    if (!e.HitAny) return;
    if (e.enemy.Dieing) { multi *= 2; }
    switch (e.enemy.AI)
    {
      case EnemyAI.FollowPath: Score += (1 * multi); break;
      case EnemyAI.Strait: Score += (2 * multi); break;
    }
  }

  #region enemy類
  Enemy instantiateEne(Vector2 p)
  {
    var bleRot = Xform.BlenderRot;
    var g = (GameObject)Instantiate(enemy, new Vector3(p.x, p.y, -60), bleRot);
    var ene = g.GetComponent<Enemy>();
    Enemies.Add(ene);
    ene.player = player;
    return ene;
  }
  void spownPathfollowEnemyOn(float x, float y)
  {
    //okonomiは270度回転した状態で現れる
    instantiateEne(new Vector2(x, 0));
  }
  Enemy spownStraitEnemy(float x, float y)
  {
    var t = instantiateEne(new Vector2(x, y));
    //enemys.Add(t);
    t.AI = EnemyAI.Strait;
    return t;
  }

  void spownWallEnemy(int count, int offsetX, int offsetY)
  {
    Enemy old = null;
    int rows = 5, cols = 5;
    int space = 8;
    var points = Enumerable.Range(0, count).Select(i => new Vector2(i % rows * space, i / cols * space));
    foreach (Vector2 v in points)
    {
      //横に
      var e = spownStraitEnemy(v.x + offsetX, v.y + offsetY);
      //if(x>) e.BrowDirection = Vector3.left;
      if (old != null) { old.BrowDirection = Quaternion.LookRotation(Xform.DifPosition(old, e)).eulerAngles.normalized; }
      old = e;
    }
  }
  enum Appeare { Normal, Inverce, Up, UpInverce, }
  IEnumerator circleS(Appeare pos, int count, float r, float xrev, float yrev)
  {
    Enemy old = null;
    var inv = 1; var range = Enumerable.Range(0, count);
    switch (pos)
    {
      case Appeare.Inverce:
        inv = -1;
        break;
      case Appeare.Up:
        range = Enumerable.Range(count / 2, count / 2);
        break;
      case Appeare.UpInverce:
        range = Enumerable.Range(count / 2, count / 2);
        inv = -1;
        break;
      default:
        break;
    }
    var s = range
        .Select(j => j * 360 / count)
        .Select(i => new Vector2(Mathf.Cos(i * Mathf.PI / 180) * r
            , Mathf.Sin(i * Mathf.PI / 180) * r * inv)
            );
    foreach (var v in s)
    {
      yield return new WaitForSeconds(0.5f);
      var e2 = spownStraitEnemy(v.x * xrev, v.y * yrev);
      if (old != null) { old.BrowDirection = Xform.Toword(e2, old).eulerAngles.normalized; }
      old = e2;
    }
  }
  IEnumerator piramidS(int count, float x)
  {
    Enemy old = null;
    int space = 4;
    var s=Enumerable.Range(0, count).Select(i => new Vector2(i * space + x, i%2==0?space:-space));
    foreach (var v in s)
    {
      yield return new WaitForSeconds(0.5f);
      var e2 = spownStraitEnemy(v.x , v.y );
      if (old != null) { old.BrowDirection = Xform.Toword(e2, old).eulerAngles.normalized; }
      old = e2;
    }
  }
  #endregion
  public int textSize = 20;
  void ShowScore()
  {
    guiText.text = "Point:" + Score.ToString();
    if ((gameEnd || GameOver) && textSize <= 68) 
    {
      textSize++; 
    }
    guiText.fontSize = textSize;
  }
  public bool gameEnd;
  bool spawnEnd;
  IEnumerator spown()
  {
    yield return new WaitForSeconds(3);//フレームで同期するならMoveNextすればいい
    //StartCoroutine(piramidS(12, 3));
    //spownPathfollowEnemyOn(-66, 0);
    //spownPathfollowEnemyOn(66, 0);
    //yield return new WaitForSeconds(2);
    //spownPathfollowEnemyOn(-66, 0);
    //spownPathfollowEnemyOn(66, 0);
    appearBuild = false;
    yield return new WaitForSeconds(2);
    spownWallEnemy(15, 15, -24);
    yield return new WaitForSeconds(3);
    spownWallEnemy(15, 12, 9);
    yield return new WaitForSeconds(9);
    StartCoroutine(circleS(Appeare.Normal, 28, 20, 1, 0.9f));
    yield return new WaitForSeconds(18);
    StartCoroutine(circleS(Appeare.Inverce, 28, 20, -1, 0.9f));
    spownPathfollowEnemyOn(-33, 23);
    spownPathfollowEnemyOn(33, 23);
    yield return new WaitForSeconds(18);

    StartCoroutine(circleS(Appeare.Normal, 4, 20, -1, 0.9f));
    StartCoroutine(circleS(Appeare.Normal, 6, 18, -1, 0.9f));
    
    spawnEnd = true;
    appearBuild = true;
    //todo 後ろから現れる敵を作る、その際にEnemyをリファクタリング
  }

  bool appearBuild = true;
  // Update is called once per frame
  Building build(Vector3 pos)
  {
    //Xform.RotLeft;
    var go = Instantiate(building, pos, Quaternion.identity) as GameObject;
    var bu = go.GetComponent<Building>();
    bu.turn = turnd;
    return bu;
  }
  public float turnd;
  IEnumerator inst;
  void FixedUpdate()
  {
    if (GameOver || gameEnd) { StopAllCoroutines(); return; }
    flame++; if (flame > 65536) flame = 0;

    if (flame % BuildTiming == 0 && appearBuild)
    {
      build(left);
      build(right);
    }
  }
  string end = "";
  void Update()
  {
	  guiText.text="";
    if (!started && StageObserver.GameOver) { StageObserver.GameOver = false; }
    if (spawnEnd && Enemies.Count == 0) gameEnd = true;
    if (started) ShowScore();
    if (gameEnd) { end = "clear"; } else if (GameOver) { end = "gameover"; }
    guiText.text += ("\n" + end);
#if UNITY_ANDROID
      guiText.text+= "atitude "+AndroidInput.direction;
      guiText.text += "touched "+AndroidInput.TuchedPos.Select(x=>x.ToString()).Aggregate((acc,x)=>acc+"\n"+x);
#endif
    if (Next) { inst.MoveNext(); }
  }
}
