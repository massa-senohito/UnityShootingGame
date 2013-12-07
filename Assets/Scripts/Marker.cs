using UnityEngine;
using System.Collections;
using System.Linq;
public class Marker : MonoBehaviour {
  public static Vector3 BackyardScreen=new Vector3(0,0,50);
  public static Marker[] Marks=new Marker[5];
  public static void SetLock(Enemy e){
    var locker=Marks.FirstOrDefault
      (m=>m.transform.position==BackyardScreen||m.Lock==null);
    if(!locker)return ;
    locker.Lockon(e);
  }
    //staticにしない
  public static void UnLock(Enemy e){
      
    var unlocker=Marks.FirstOrDefault
      (m =>
      {
          if (m.Lock == null) return true;
          return m.Lock.Equals(e);
      });
    if(!unlocker)return ;
    //print("unlocked");
    unlocker.transform.position=BackyardScreen;
    unlocker.Lock=null;
  }
  Enemy Lock;
  public void Lockon(Enemy e)
  {
      Lock = e;
      transform.position = e.transform.position;
  }
  // Use this for initialization
  void Start () {  }
  
  public override string ToString(){
    return "marker" + transform.position;
  }
  // Update is called once per frame
  void Update () {
      if (Lock == null) { transform.position = BackyardScreen; return; }
    transform.position=Lock.transform.position;
  }

}
