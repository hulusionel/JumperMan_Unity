using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoruScript : MonoBehaviour {

	public void DogruCevap()
    {
        Invoke("Dogru", 1);
    }
    public void YanlisCevap()
    {
        Invoke("Yanlis", 1);
    }

    void Dogru()
    {
        SceneManager.LoadScene("sahne");
    }
    void Yanlis()
    {
        SceneManager.LoadScene("AnaMenu");
    }

	void Start () {
		
	}
	
	
	void Update () {
		
	}
}
