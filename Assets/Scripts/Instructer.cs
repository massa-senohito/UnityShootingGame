using UnityEngine;
using System.Collections;
[RequireComponent(typeof(GUITexture))]

public class Instructer : MonoBehaviour {
  public static Instructer main;
  public Texture2D[] insts=new Texture2D[6];
	// Use this for initialization
  Rect instr;
	void Start () {
    main = this;//310,150,3,-190,Screenは640,384
    var ntex = new Rect(Screen.width/2, Screen.height/2.56f, 3, -190);
    instr = ntex;
    var inst1re = new Rect(329, 130, 33, -142);
    var inst2re = new Rect(329, 57, 0, -115);
    var inst4re = new Rect(329, 130, 20, -160);
    var inst5re = new Rect(326, 130, 32, -194);
    var inst6re = new Rect(329, 130, 6, -218);
    //instr=new Rect[]{inst1re,inst1re,inst1re,inst1re,inst5re,inst6re};
	}
  public void PrintInstruct(int index)
  {
    guiTexture.texture = insts[index];
    guiTexture.pixelInset = instr;
  }
  public void Clear()
  {
    Destroy(gameObject);
  }
	// Update is called once per frame
	void Update () {
	
	}
}
