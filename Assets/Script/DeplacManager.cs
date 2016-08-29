using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeplacManager : MonoBehaviour {
	public float frequency = 0.1f;
	public float miniDist = 0.1f;

	private Vector3 BeforePos = new Vector3 ();
	private Vector3 BeforePosWorld = new Vector3 ();

	private Vector3 Deplacement = new Vector3 ();
	private bool check = false;

	public string main;

	void OnEnable () {
		//Debug.Log ("Enable");
		check = true;
		StopCoroutine (MaCoroutine ());
		StartCoroutine (MaCoroutine ());

	}

	IEnumerator MaCoroutine() {
		while (true) {
			yield return new WaitForSeconds (frequency);
			if (!check) {
				Deplacement = BeforePosWorld - transform.position;
				if (Deplacement.magnitude > miniDist)
					CreateMultipleForce ();
			}
			check = false;
			BeforePos = transform.localPosition;
			BeforePosWorld = transform.position;
			yield return null;
		}
	}

	void CreateMultipleForce() {
		//if (Deplacement.magnitude < 0.1f)
		CreateAndSendMouvement (BeforePosWorld);

		/*else {
			Vector3 Positions = new Vector3 ();
			for (int c = 1; c <= 3; c++) {
				Positions = (c / 3 * Deplacement) + BeforePos;
				CreateAndSendMouvement (Positions);
			}
		}
		*/
	}

	void CreateAndSendMouvement(Vector3 Pos) {

		Debug.Log("CreateAndSendMouvement");

		GameObject child = Instantiate (Resources.Load ("Prefabs/Force", typeof(GameObject))) as GameObject;

		child.transform.Rotate (transform.rotation.eulerAngles);
		child.transform.position = Pos;
		child.GetComponent<Force> ().SendForce (main);
	}
}
