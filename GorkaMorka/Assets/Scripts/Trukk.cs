using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Trukk : MonoBehaviour {


		PlayerController playerController;
		Tiles_Layer tiles_Layer;

		GameObject SlowSpeed;


		bool modelSelected;

		public int currentTileNum;
		public Transform currentTileTrans;

		private bool modelMove = false;

		public float moveSpeed = 4f;
		public float turnSpeed = 5f; 
		public Transform startingTile;
		public Transform targetTileTransform;
		public Vector3 vectorToTarget;





		// Use this for initialization
		void Start () {

			playerController = GameObject.Find ("Models_Layer").transform.GetComponent<PlayerController> ();


			float x = this.transform.parent.position.x + 1.0f;
			float y = this.transform.parent.position.y + 1.0f;
			float z = this.transform.parent.position.z + 0.0f;
			Quaternion newRotation = Quaternion.Euler(0,0,0);

			//this.transform.position = new Vector3(x, y, z);
			this.transform.position = startingTile.transform.position;
			this.transform.rotation = newRotation;

			currentTileTrans = startingTile;
			moveSpeed = 4f;

		}

		// Update is called once per frame
		void Update () {

			if(modelMove) {

				if (vectorToTarget.magnitude <= 0) {

					float step = moveSpeed * Time.deltaTime;

					transform.position = Vector3.MoveTowards (this.transform.position, targetTileTransform.transform.position, step);
				}
			}
		}




		void OnMouseDown() {


			// IF a model IS selected //
			if (playerController.modelSelected) {

				playerController.PlayerDeSelected ();

				MovementAreaClear();

				// IF a model IS NOT selected //
			} else {

				playerController.PlayerSelected (true, this.gameObject, currentTileTrans.gameObject, currentTileNum);

				MovementAreaDisplay();

			}
		}


		public void ModelMove(GameObject tile) {

			targetTileTransform = tile.transform;

			// SOME TURNING FUCKING CUNTION ///
			//	Vector3 vect = new Vector3(target.position.x, target.position.y, target.position.z);
			//	transform.rotation = Quaternion.AngleAxis(30, vect);

			modelMove = true;

		}


		// Reveal area player can move //
		void MovementAreaDisplay() {

			playerController.PlayerMovementAreaDisplay (this.currentTileTrans, this.currentTileNum);
		}

		// Reveal area player can move //
		void MovementAreaClear() {

			playerController.PlayerMovementAreaClear ();
		}

	}
