using UnityEngine;
using System.Collections;
public class Rotato : MonoBehaviour {
	Transform mTransform;
	void Start()
	{
		mTransform = transform;
	}

	// Update is called once per frame
	void Update () {
		mTransform.LookAt(mTransform.position + NormMouse.Instance.GetNormalizedDirectionToMouse());
	}
}