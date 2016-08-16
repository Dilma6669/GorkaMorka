using UnityEngine;
using System.Collections;

public class Tiles : MonoBehaviour {

	GameMaster gameMaster;

	PlayerController playerController;

	Model modelScript;

	public int tileCounter;

	SpriteRenderer rend;

	int[] playerPostionXY;
	int newPlayerX;
	int newPlayerY;


	public bool tileSelected;
	public Sprite Hex_Green_Inner;
	public Sprite Hex_Green_Outer;
	public Sprite Hex_Black_Outer;


	Transform[] movementSpreadArray = new Transform[5];

	// Use this for initialization
	void Start () {

		gameMaster = GameObject.Find ("GameMaster").transform.GetComponent<GameMaster> ();
		playerController = GameObject.Find ("Models_Layer").transform.GetComponent<PlayerController> ();

		// Make tile transparent //
		rend = GetComponent<SpriteRenderer> ();
		rend.enabled = false;
		//////////////////////////

		// Set Unique Tile Counter //
		tileCounter = gameMaster.uniqueTileCounter;
		tileCounter += 1;
		gameMaster.uniqueTileCounter = tileCounter;
		///////////////////////////////

	}


	// Update is called once per frame
	void Update () {

	}


	void OnMouseDown() {

		///////////////////////////////////////////////////////
		// Working out X Y tile coordinates (not important) //
		int hexSize = gameMaster.hexSize - 1;

		// Working out new Y position //
		int newPlayerY = tileCounter % hexSize;
		if (newPlayerY == 0) {
			newPlayerY = hexSize;
		}
		// Working out new X position //
		int newPlayerX = (tileCounter/hexSize)+1;
		if (newPlayerY == hexSize) {
			newPlayerX = (tileCounter/hexSize);
		}
		/////////////////////////////////////////////////////

		// zero out array //
		//((IList)movementSpreadArray).Clear ();


		if (playerController.modelSelected) {

			int count = playerController.arrayCount;

			movementSpreadArray = playerController.MovementSpreadArray;

			for (int i = 0; i < count; i++) {
				if (this.transform == movementSpreadArray [i]) {
			

					tileSelected = true;
					playerController.PlayerMove (true, this.gameObject, tileCounter);

				}
	
			}
		}


			tileSelected = false;
			
	} 
		



	public void TileSelect() {

		rend.enabled = true;
		rend.sprite = Hex_Green_Inner;

	}


	public void TileDeSelect() {

		// This has to be fixed //  <============
		rend.sprite = Hex_Green_Outer;
		//rend.enabled = false;
	}


	public void TileInMovementArea(){

		rend.enabled = true;
		rend.sprite = Hex_Green_Outer;
	}

	public void TileOutMovementArea(){

		rend.enabled = false;
		//rend.sprite = Hex_Green_Outer;
	}



	void OnTriggerEnter(Collider other) {

	/*	if (other.tag == "Player") {
			rend.enabled = true;
			rend.sprite = Hex_Green_Outer;
		}
			*/
	}	

	void OnTriggerExit(Collider other) {
		/*

		//if(other.tag == "Player") {
			rend.enabled = false;

	//	} */

	} 

		
}
