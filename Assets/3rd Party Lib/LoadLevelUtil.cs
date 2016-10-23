using UnityEngine;
using System.Collections;

public class LoadLevelUtil : MonoBehaviour {

	public void LoadLevel (int level){
		Application.LoadLevel(level);
	}
}
