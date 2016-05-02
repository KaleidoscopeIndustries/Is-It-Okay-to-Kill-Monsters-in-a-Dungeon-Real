using UnityEngine;
using System.Collections;

public class Wavetime : MonoBehaviour {

   
    public GameObject[] enemies;                
    public float spawnTime = 2f;            
    public Transform[] spawnPoints;
    public int wave;
    public int maxnumberofenemies = 0;
    public int currentnumberofenemies = 0;
	public int score = 0; //keeping track of initial score




    void OnTriggerExit()
    {
		
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<BoxCollider>().isTrigger = false;
        Wavesetup();
    }

    void Wavesetup()
    {
        wave = 1;
		GameObject wn = GameObject.Find ("WaveNumber");
		wn.GetComponent<UnityEngine.UI.Text>().text = "Wave: " + wave;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        
    }

    void Spawn()
    {

        if (maxnumberofenemies < wave * 10)
        {
            maxnumberofenemies += 1;
            currentnumberofenemies += 1;
            int enemy = Random.Range(0, enemies.Length);
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);


            Instantiate(enemies[enemy], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        if (currentnumberofenemies == 0 && maxnumberofenemies >= wave*10 -1)
        {
            maxnumberofenemies = 0;

			score += 100; //arbitrary number to be added at the end of each wave. Change if necessary
			GameObject sc = GameObject.Find ("Score"); //retrieving values for the UI
			sc.GetComponent<UnityEngine.UI.Text> ().text = "Score: " + score; //setting it in the UI
			GameObject wn = GameObject.Find ("WaveNumber");
            wave++;
			wn.GetComponent<UnityEngine.UI.Text>().text = "Wave: " + wave;
        }
        
    }
}
