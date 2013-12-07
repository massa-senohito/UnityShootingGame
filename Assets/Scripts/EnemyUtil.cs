using UnityEngine;
using System.Collections;
public static class EnemyUtil
{

  public static Vector3 nextToDo(Enemy e)
  {
    if (e.AI == EnemyAI.FollowPath)
    {
	  //接触時など相互判定してたり、相互再帰に気をつける
      iTween.PutOnPath(e.gameObject, e.points, Tween.percent(e.per));
      return e.transform.position;
    }
    else
    {
      //perで移動させたほうがいいかも
      return e.transform.position += new Vector3(0, 0, 0.1f);
    }

  }
}
public enum EnemyAI
{
  Strait,
  FollowPath,
}
