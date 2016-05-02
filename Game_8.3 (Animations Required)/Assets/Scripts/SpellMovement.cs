using UnityEngine;
using System.Collections;

public class SpellMovement : MonoBehaviour {
	
	public float speed;
	
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Rigidbody> ().velocity = this.transform.parent.rotation * Vector3.left * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
