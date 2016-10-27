using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public string postScoreURL = "http://172.99.106.210:8080/data.php?";
	public string userName;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start () {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update () {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves () {

		yield return new WaitForSeconds (startWait);

		while (true) {
			for (int i =0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				Debug.Log ("finalScore: " + score);
				userName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
				string post_url = postScoreURL + "gameover=" + gameOver + "&name=" + WWW.EscapeURL(userName) + "&score=" + score;
				Debug.Log ("post_url: " + post_url);
				Debug.Log ("name:" + userName);
				// Post the URL to the site and create a download object to get the result.
				WWW hs_post = new WWW(post_url);
				yield return hs_post; // Wait until the download is done
				
				if (hs_post.error != null)
				{
					print("There was an error posting the high score: " + hs_post.error);
				}
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public void GameOver () {
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
