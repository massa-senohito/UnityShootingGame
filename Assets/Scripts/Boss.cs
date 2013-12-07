//����łƃ����O���Ŕ���
//�e���G��3��܂ł͂˂܂��
//��ɂ�����@���΂𕬂��@���ǂ𒣂�@���v���[���[�̑O�Ő���
using System.Collections;
using UnityEngine;
enum BossAI
{
    GoUp, Fire, Wall, Turn
}
class BossObserver:MonoBehaviour
{
  public PlayerBehav player;
  public Boss boss;
  public int wallCount;
  public Enemy wall;
  void SpawnWall()
  {
    //�Б������o��������
    var nearByBoss=boss.transform.position+Random.insideUnitSphere;
    if(wallCount<6){
      Instantiate(wall, nearByBoss, Xform.BlenderRot);
      wallCount++;
    }
  }
  
  private void Enemy_OnDead(object sender, EnemyDeadEventArgs e){
    //e.Enemy
    wallCount--;
  }
  void OnDestroy(){
    Enemy.OnDead-=Enemy_OnDead;
  }
  void Start(){
    Enemy.OnDead += Enemy_OnDead;
  }
  void Update(){
  }
}
class Boss:MonoBehaviour
{
    public int CycleSpeed;
    public BossAI ai;
    public Vector3 upPosition=new Vector3(0,-12,50);
    public GameObject wall;
    IEnumerator PatternCycle()
    {
        ai = BossAI.GoUp;
        yield return new WaitForSeconds(CycleSpeed);
        ai = BossAI.Fire;
        yield return new WaitForSeconds(CycleSpeed);

    }
    void Goup(){
        animation.GetClip("");
        iTween.MoveFrom(gameObject,upPosition,4);
    }

    void Update()
    {
        switch (ai)
        {
            case BossAI.GoUp:
                Goup();
                break;
            case BossAI.Wall:
                break;
            default:
                break;
        }
    }
}
