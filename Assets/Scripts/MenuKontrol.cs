using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuKontrol : MonoBehaviour {

    public Text txthighScore;
	public void Oyna()
    {
        SceneManager.LoadScene("sahne"); //play tuşu basıldığında oyuna en baştan başlar
    }

    public void Sifirla()
    {
        PlayerPrefs.SetInt("HighScoreKayit", 0);//yüksek skor sıfırlandı
        PlayerPrefs.SetInt("soruSayac", 0); //2 oyunda bir soru sorulması lazım onuda sıfırlar
        txthighScore.text ="High Score = " +PlayerPrefs.GetInt("HighScoreKayit").ToString();
    }

    public void Cikis()
    {
        Application.Quit();
    }

    void Start ()
    {
        int highScore = PlayerPrefs.GetInt("HighScoreKayit");
        txthighScore.text = "High Score = " + highScore;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
