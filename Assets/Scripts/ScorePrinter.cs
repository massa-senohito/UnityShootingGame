using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class ScorePrinter : MonoBehaviour {
    //todo 
	// Use this for initialization
	void Start () {
        var one = renderer.materials[0];
        var two = renderer.materials[1];
        renderer.material = one;
	}
	
	// Update is called once per frame
	void Update () {

        Xform.Raise( this,0.1f);
	}
}
