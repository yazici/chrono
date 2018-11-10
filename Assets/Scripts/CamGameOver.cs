﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamGameOver : MonoBehaviour {

	public Camera cam;
	public PlayerMovementController playerWon;
	public Color textColor;

	public float zoomMin;
	public float startingZoom;
	public float zoomSpeed;

	public GameObject deathConfetti; 
	public GameObject letterbox;
	public TextMesh winnerText;

	public AudioClip pop;
	public AudioClip fireworks;
	public AudioClip slam;

	public int winnerTextTime;
	public int explosionSoundTime;
	public int fireworksSoundTime;

	bool fireworksSound;
	bool winnerTextDisplayed;
	bool explosionSound;

	int counter = 0;

	// Use this for initialization
	void Start () {

		cam = GetComponent<Camera> ();
		startingZoom = cam.orthographicSize;
		Instantiate (deathConfetti, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);

		if (playerWon.colorName == "red") {
			GameMaster.me.redSets ++;
			GameMaster.me.player1.rb.isKinematic = false;
		} if (playerWon.colorName == "blue") {
			GameMaster.me.blueSets++;
			GameMaster.me.player2.rb.isKinematic = false; 
		}



		
	}
	
	// Update is called once per frame
	void Update () {

		counter++;
		if (cam.orthographicSize > zoomMin) {
			cam.orthographicSize -= Mathf.Pow (zoomSpeed, 2);
		}

		if (counter > winnerTextTime && !winnerTextDisplayed) {

			TextMesh winner = Instantiate (winnerText, new Vector3 (transform.position.x, transform.position.y + .3f, -8), Quaternion.identity);
			winner.text = playerWon.colorName + "\nhas won";
			winner.color = playerWon.playerColor;
			GameMaster.me.updateUI ();
			SoundController.me.PlaySound (slam, 1f);
			this.gameObject.GetComponent<Screenshake> ().SetScreenshake (1.5f, 0.8f);
			winnerTextDisplayed = true;

		}

		if (counter > explosionSoundTime && !explosionSound) {

			this.gameObject.GetComponent<Screenshake> ().enabled = true;
			this.gameObject.GetComponent<Screenshake> ().defaultCameraPos = transform.position;
			this.gameObject.GetComponent<Screenshake> ().SetScreenshake (1.5f, 0.8f);
			SoundController.me.PlaySound (pop, 1f);
			explosionSound = true;

		}

		if (counter > fireworksSoundTime && !fireworksSound) {

			SoundController.me.PlaySound (fireworks, 1f);
			fireworksSound = true;

		}

		bool redWon = GameMaster.me.redSets >= GameMaster.me.setsNeeded;
		bool blueWon = GameMaster.me.blueSets >= GameMaster.me.setsNeeded;

		if (redWon || blueWon) {
			GameMaster.me.matchOver = true;
		}

		if ((counter > fireworksSoundTime + 150) && (redWon || blueWon)) {

			GameMaster.me.winner = playerWon.colorName;
			Time.timeScale = 1f;
			cam.transform.position = new Vector3(0, 2.42f, -10);
			Destroy(letterbox);
			cam.orthographicSize = startingZoom;
			GameMaster.me.enableMatchOver();
			this.enabled = false;
			//UnityEngine.SceneManagement.SceneManager.LoadScene ("gameover2");


		}
		
	}
}
