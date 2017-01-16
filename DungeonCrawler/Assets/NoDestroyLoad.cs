using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyLoad : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this.gameObject);
		Destroy(this);
	}
}
