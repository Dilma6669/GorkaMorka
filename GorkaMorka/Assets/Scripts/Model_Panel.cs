using UnityEngine;
using System.Collections;

public class Model_Panel : MonoBehaviour {

	PlayerController playerController;

	Rect rectWalk;
	Rect rectRun;
	Rect rectOver;

	Transform tileTrans;
	int tileNum;

	// Use this for initialization
	void Start () {

		playerController = GameObject.Find ("Models_Layer").transform.GetComponent<PlayerController> ();

		rectWalk = new Rect (10, 10, 200, 60);

		rectRun = new Rect (10, 80, 200, 60);

		rectOver = new Rect (10, 150, 200, 60);

			
	}

	// Update is called once per frame
	void Update () {

	}


	bool OverGui() {

		Vector3 mouse = Input.mousePosition;

		return rectWalk.Contains (mouse) || rectRun.Contains (mouse) || rectOver.Contains (mouse);
	}


	void OnGUI() {

		tileTrans = playerController.ModelsTileObject.transform;
		tileNum = playerController.modelsTileNum;

		if (GUI.Button (rectWalk, "Walk")) {

			Debug.Log ("Walk button clicked");

			// Clear moveable area first, change movement type, then Display area //
			playerController.PlayerMovementAreaClear ();
			// 1 = walking //
			playerController.moveType = 1;
			playerController.PlayerMovementAreaDisplay(tileTrans, tileNum);
			///////////////////////////////////////////////

		}

		if (GUI.Button (rectRun, "Run")) {

			Debug.Log ("Run button clicked");

			// Clear moveable area first, change movement type, then Display area //
			playerController.PlayerMovementAreaClear ();
			// 2 = running //
			playerController.moveType = 2;
			playerController.PlayerMovementAreaDisplay(tileTrans, tileNum);
			////////////////////////////////////////////
		}

		if (GUI.Button (rectOver, "OverWatch")) {

			Debug.Log ("OverWatch button clicked");

			// Clear moveable area first, change movement type, then Display area //
			playerController.PlayerMovementAreaClear ();
			// 3 = overwatch //
			playerController.moveType = 3;
			playerController.PlayerMovementAreaDisplay(tileTrans, tileNum);
			/////////////////////////////////////////
		}
	} 
}
