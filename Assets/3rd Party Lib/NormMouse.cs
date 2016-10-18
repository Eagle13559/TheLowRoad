using UnityEngine;
using System.Collections;

public class NormMouse : MonoBehaviour {

	public static NormMouse Instance;
	void Awake() { Instance = this; }

	Vector3 viewPortActual, viewPortDelta, directionToMouse;

	/// <summary>
	/// Retrieves a Normalized Vector3 Direction from mPosition towards the MousePos
	/// </summary>
	/// <returns>a Normalized Vector3 Direction from mPosition towards the MousePos (as WorldPos)</returns>
	public Vector3 GetNormalizedDirectionToMouse()
	{
		// Calculate a direction based on the Mouse Position (using Viewport)
		viewPortActual = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		viewPortDelta = new Vector3(0.5f, 0.5f, 0); // Detract Half the screen
		directionToMouse = viewPortActual - viewPortDelta; // Caculate Direction
		return directionToMouse.normalized; // Return normalized Direction
	}
}