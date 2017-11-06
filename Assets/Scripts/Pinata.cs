﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinata : MonoBehaviour {

	public float health;
	public float startingHealth;
	bool exploded;
	public GameObject bullet;
	public float explosionSpeed;

	public ParticleSystem confetti;
	public AudioClip pop;

	// Use this for initialization
	void Start () {

		startingHealth = health;
		GrowAnimation ();

	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0 && !exploded) {
			exploded = true;

			for (int i = 1; i < Random.Range (10, 20); i++) {
				
				GameObject tempBullet = Instantiate (bullet, new Vector2 (transform.position.x + Random.Range (-1, 1), transform.position.y + Random.Range (-1, 1)), Quaternion.identity);
				Bullet2 bulletTemp = tempBullet.GetComponent<Bullet2> ();
				//bulletTemp.decayColor = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
				//bulletTemp.decayed = true;
				bulletTemp.vel = new Vector2 (Random.Range (-explosionSpeed, explosionSpeed), Random.Range (-explosionSpeed, explosionSpeed));

			}
			Camera.main.GetComponent<Screenshake> ().SetScreenshake (2f, .5f);
			SoundController.me.PlaySound (pop, 1.5f);
			Instantiate (confetti, transform.position, Quaternion.identity);
			Destroy (this.gameObject);

		}
			
		transform.localScale = new Vector2(2.1f, 2.1f) * (health / startingHealth);

		
	}

	void OnTriggerEnter2D(Collider2D coll) {

//		Debug.Log ("pinata hit");
		if (coll.gameObject.tag == "bullet") {

			//health -= 1;

		}


	}

	void GrowAnimation() {

	}


}
