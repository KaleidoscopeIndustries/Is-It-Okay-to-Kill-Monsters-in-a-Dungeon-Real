using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	public float speed;

    private Rigidbody2D rb;
	
	private int move;
	private float moveHorizontal;
	private float moveVertical;

	void Start()
	{
        rb = GetComponent<Rigidbody2D>();
	//	count = 0;
	//	SetCountText (); not a factor anymore
		
	}
	void Update ()
	{
		if (Input.GetKey ("a")) {
			transform.forward = new Vector3(0f, 0f, 1f);
		}
		if (Input.GetKey ("d")) {
			transform.forward = new Vector3(0f, 0f, -1f);
		}
	}
	
	void FixedUpdate ()
	{
		
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3(0.0f, 0.0f, moveHorizontal);

		rb.AddForce (movement * speed);
		if (moveHorizontal > 0 || moveVertical > 0)
		{
			move++;
		}
	}
	//old win method
	/*void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "pickup");
		
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}
	
	void SetCountText ()
	{
		countText.text = "Count:" + count.ToString ();
		if (count >= 1) {
			winText.text = "Player wins!";
		}
*/
}