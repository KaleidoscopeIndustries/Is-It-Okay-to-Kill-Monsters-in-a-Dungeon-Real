using UnityEngine;
using System.Collections;

public class move1 : MonoBehaviour {
	
	public int HP = 100;
		
	public int speed = 5;

	private Rigidbody rb;

	public float fireRate, iceRate, tclapRate, shieldRate;
	
	private float nextFire, nextIce, nextTclap, nextShield;

	public Transform SpellSpawn;
	public Transform spellSpawn2;
	
	private Vector3 origin;

	public AudioSource[] sounds;
	public AudioSource fball;
	public AudioSource iwal;
	public AudioSource tclap;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		GameObject hs = GameObject.Find ("High Score");
		hs.GetComponent<UnityEngine.UI.Text> ().text = "High Score: " + PlayerPrefs.GetInt ("highscore");

		sounds = GetComponents<AudioSource> ();
		fball = sounds [0];
		iwal = sounds [1];
		tclap = sounds [2];

	}

	void Update()
	{
		GameObject slider = GameObject.Find ("HP");
		slider.GetComponent<UnityEngine.UI.Slider> ().value = HP;

		if (Input.GetKey ("a")) {
			transform.forward = new Vector3 (0f, 0f, 1f);
		}
		if (Input.GetKey ("d")) {
			transform.forward = new Vector3 (0f, 0f, -1f);
		}
	}
	
	void FixedUpdate ()
	{

		origin = this.transform.position;
		
		float moveHorizontal = Input.GetAxis ("Horizontal");
		
		float moveUp;
		
		if (Input.GetKey (KeyCode.W)) {
			moveUp = 3.0f;
		} else
			moveUp = -1.0f;
		
		if (Input.GetKey (KeyCode.Space)) {
			moveHorizontal = 0;
			moveUp = 0;
		}
		
		Vector3 movement = new Vector3 (moveHorizontal, moveUp, 0.0f);
		
		rb.AddForce (movement * speed);
		
		if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time > nextShield)
		{ Shield();
			nextShield = Time.time + shieldRate;}
		
		if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time > nextFire)
		{ Fireball();
			nextFire = Time.time + fireRate;} 
		
		if (Input.GetKeyDown(KeyCode.DownArrow) && Time.time > nextIce)
		{ Icenova();
			nextIce = Time.time + iceRate;}
		
		if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time > nextTclap)
		{ Thunderclap();
			nextTclap = Time.time + tclapRate;}
	}
	
	void Fireball ()
	{
		// make a fireball that moves out in front of the player

		GameObject fireballspell = (GameObject)Instantiate(Resources.Load("fireball"), origin, SpellSpawn.rotation);
		//fireballspell.GetComponent<Rigidbody>().AddForce(transform.forward*3);
		fireballspell.transform.parent = this.transform;
		fball.Play ();
		Destroy (fireballspell, 0.4f);
		//remove at boundary
	}
	
	void Icenova ()
	{
		// make two nova instances, going in opposite directions
		
		//instantiate 1
		GameObject novaleftspell = (GameObject)Instantiate(Resources.Load("novaleft"), origin + Vector3.left, Quaternion.identity);

		//instantiate 2
		GameObject novarightspell = (GameObject)Instantiate(Resources.Load("novaright"), origin + Vector3.right, Quaternion.identity);

		iwal.Play ();
		//compare tag, 40 damage
			Destroy (novaleftspell, 0.5f);
			Destroy (novarightspell, 0.5f);
		//remove at distance from origin point (duration?)
	}
	
	void Thunderclap ()
	{
		//make one blast instance in front of the player with speed of 0
		
		//instantiate
		GameObject tclapspell = (GameObject)Instantiate(Resources.Load("tclap"), origin, spellSpawn2.rotation);
		tclap.Play ();
		Destroy (tclapspell, 0.1f);

		
		//compare tag, 100 damage
		
		//remove after duration
	}
	
	void Shield ()
	{
		// restore HP to the player
		HP += 75;
        if (HP >= 100)
        {
            HP = 100;
        }
	}
	void OnTriggerEnter (Collider other)
	{
		Animator animator;


		if (other.tag == "ogre") {

			animator = other.gameObject.GetComponent<Animator> ();
			animator.SetBool("IsWalking", false);
			animator.SetBool("IsAttacking", true);

			HP -= 20;

		}
		if (other.tag == "skeleton") {
			HP -= 15;
		}
		if (other.tag == "wolf") {
			HP -= 10;
		}
		if (other.tag == "troll") {
			HP -= 40;
		}
		if (HP <= 0) {
			GameObject sc = GameObject.Find ("Score");
			string temp = sc.GetComponent<UnityEngine.UI.Text> ().text;
			string score = temp.Substring (6);
			int curScore = int.Parse (score);
			OnPlayerDeath (curScore);
			Application.LoadLevel(0);
		}
	}

	void OnTriggerExit (Collider other)
	{
		Animator animator;

		if (other.tag == "ogre") {

			animator = other.gameObject.GetComponent<Animator> ();
			animator.SetBool("IsWalking", true);
			animator.SetBool("IsAttacking", false);
		}
	}

	void OnPlayerDeath (int curScore)
	{
		

		int oldscore = PlayerPrefs.GetInt ("highscore", 0);
		if (curScore > oldscore) {
			PlayerPrefs.SetInt ("highscore", curScore);
			PlayerPrefs.Save ();
		}	


		

	}

}