using UnityEngine;
using System.Collections;

public class ProjectileDrag : MonoBehaviour{

	public float maxStretch = 3f;
	private float maxStretchSqr;
	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;
	public GameObject catapult;
	private SpringJoint2D spring;
	private bool clickedOn;
	private Ray rayToMouse;

	void Awake() {
		spring = GetComponent<SpringJoint2D>();
	}

	void Start(){
		LineRendererSetup();
		rayToMouse = new Ray(catapult.GetComponent<Rigidbody2D>().position, Vector2.zero);
		maxStretchSqr = maxStretch * maxStretch;
	}

	void Update(){
		if (clickedOn) {
			Dragging();
		}
	}

	void LineRendererSetup(){
		catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition(0, catapultLineBack.transform.position);

		catapultLineFront.sortingLayerName = "Foreground";
		catapultLineBack.sortingLayerName = "Foreground";

		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;
	}

	void LineRendererUpdate() {
		
	}

	void OnMouseDown() {
		spring.enabled = false;
		clickedOn = true;
	}

	void OnMouseUp() {
		spring.enabled = true;
		clickedOn = false;
		GetComponent<Rigidbody2D>().isKinematic = false;
	}

	void Dragging() {
		Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.GetComponent<Rigidbody2D>().position;

		if (catapultToMouse.sqrMagnitude > maxStretchSqr) {
			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
		}

		transform.position = mouseWorldPoint;
	}
}