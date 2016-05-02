//Robert Forbes
using UnityEngine;
using System.Collections;

public class TrollCode: MonoBehaviour 
{
	private Rigidbody rigid;
	public Transform Player;
	public float speed;
	private float Speed = 10000.0f;
	
	private int health = 200;

	public AudioSource[] sounds;
	public AudioSource TrollLive;
	public AudioSource TrollDie;
	
	void Start() 
	{
		rigid = gameObject.GetComponent <Rigidbody> ();
		Player = GameObject.FindWithTag ("Player").transform;

		sounds = GetComponents<AudioSource> ();
		TrollLive = sounds [0];
		TrollDie = sounds [1];
	}

	void Awake ()
	{
		TrollLive.Play ();
	}
	
	void Update() 
	{
		transform.position = Vector3.Lerp (transform.position, Player.position, Time.deltaTime * speed);
		
		if (Player.position.x < transform.position.x) {
			transform.forward = new Vector3 (0f, 0f, 1f);
		}
		if (Player.position.x > transform.position.x) {
			transform.forward = new Vector3 (0f, 0f, -1f);
		}
		
		if (health <= 0) {

			TrollDie.Play ();
			GameObject cu = GameObject.Find ("Cube");
			cu.GetComponent<Wavetime> ().currentnumberofenemies -= 1;
			Destroy (this.gameObject);
			
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Jump" && Player.position.y > transform.position.y) 
		{
			rigid.AddForce (Vector3.up * Speed * Time.deltaTime);
			Debug.Log ("Did it jump?");
		}
		if (other.tag == "Player") 
		{
			
		}
		if (other.tag == "fireball") 
		{
			health -= 75;
			GameObject blastspell = (GameObject)Instantiate(Resources.Load("fireblast"), Vector3.zero, Quaternion.identity);
			Destroy(blastspell, 0.1f);
		}
		if (other.tag == "fireblast") {
			health -= 75;
		}
		if (other.tag == "nova") {
			health -= 50;
		}
		if (other.tag == "tclap") {
			health -= 75;
		}
	}
}
