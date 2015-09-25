using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {

	public float power;
	private float angle, angleSpeed, angleLimit, powerPercent, powerSpeed, powerLimitMin, powerLimitMax;
	private bool rotatingUp, maxPower, angleLocked, powerLocked;
	private int clicks;
	public Rigidbody2D projectile;
	public GameObject projectileObject;
	
	void Start () {
		angleLocked = false;
		powerLocked = false;
		rotatingUp = true;
		maxPower = false;
		angle = 0;
		angleSpeed = 1;
		angleLimit = 45;
		powerLimitMin = 0.5f;
		powerLimitMax = 1.0f;
		powerPercent = powerLimitMin;
		powerSpeed = 0.01f;
	}
	
	void Update () {
		// Permitir um único lançamento
		if (angleLocked == false && powerLocked == false) {
			LaunchMissile();
		} else if (angleLocked && powerLocked == false) {
			LaunchMissile();
		}

		VaryAngle();
		if (angleLocked) {
			VaryPower();	
		}
	}

	// Lança o projétil
	void LaunchMissile() {
		if (Input.GetMouseButtonDown(0)) {
			angleLocked = true;
			clicks++;
			if (clicks == 2) {
				// Pra permitir um lançamento futuro tem que resetar o clicks
				powerLocked = true;
				power = power * powerPercent;
				projectile.transform.Rotate(0, 0, angle);
				projectile.AddRelativeForce(new Vector2(power, 0));
				projectile.gravityScale = 1;
			}
		}
	}

	private void VaryAngle() {
		// ifs para fazer a seta alternar até o ângulo limite
		if (angle >= -angleLimit && angle <= angleLimit && rotatingUp && angleLocked == false) {
			RotateUp();
			if (angle == angleLimit) {
				rotatingUp = false;
			}
		}
		if (angle >= -angleLimit && angle <= angleLimit && rotatingUp == false && angleLocked == false) {
			RotateDown();
			if (angle == -angleLimit) {
				rotatingUp = true;
			}
		}
	}

	private void VaryPower() {
		// Alterar o poder do lançamento
		if (powerPercent >= powerLimitMin && powerPercent <= powerLimitMax && maxPower == false && powerLocked == false) {
			ScaleUp();
			if (powerPercent >= powerLimitMax) {
				maxPower = true;
			}
		}
		if (powerPercent >= powerLimitMin && (int)powerPercent <= powerLimitMax && maxPower && powerLocked == false) {
			ScaleDown();
			if (powerPercent <= powerLimitMin) {
				maxPower = false;
			}
		}
	}

	private void ScaleUp() {
		Vector3 scale = transform.localScale;
		powerPercent += powerSpeed;
		scale.x = powerPercent;
		transform.localScale = scale;
	}
	private void ScaleDown() {
		Vector3 scale = transform.localScale;
		powerPercent -= powerSpeed;
		scale.x = powerPercent;
		transform.localScale = scale;
	}

	private void RotateUp() {
		transform.Rotate(0, 0, angleSpeed);
		angle += angleSpeed;
	}
	private void RotateDown() {
		transform.Rotate(0, 0, -angleSpeed);
		angle -= angleSpeed;
	}

}
