using UnityEngine;
using System.Collections.Generic;


public class Tween{
        GameObject mover;
        public Vector3[] goal;
        public Tween(GameObject move,Vector3 goal)
        {
            mover = move;
            this.goal = new Vector3[1];
            this.goal[0] = goal;
            iTween.Init(move);
        }
        public static float percent(float p)
        {
            return Mathf.Clamp(p + 0.02f, 0, 1);
        }
	
        public float Step(float p)
        {
	      iTween.PutOnPath(mover,goal,p);
	    return percent(p);//todo 要動作検証
        }
	public void GoIn(float time){iTween.MoveTo(mover,goal.Last(),2);}
} 

