using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KarakterKontrol : MonoBehaviour
{
    public Sprite[] kosma;
    public Sprite[] ziplama;
    public Text txtScore;
    public Text txtHighScore;
    public AudioClip ZiplamaSes;
    public AudioClip CarpmaSes;
    public AudioClip PuanSes;

    SpriteRenderer sp_renderer;
    Rigidbody2D fizik;
    OyunKontrol oyunKontrol;

    AudioSource ses;

    bool tekZiplama = true;
    static bool gameOver = false;
    float kosmaAnimZaman = 0;

    int runAnim = 0;   
    int puan = 0;
    int highScore = 0;
    int soruCikmaSayac = 0;    

    void Start()
    {
        sp_renderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunKontrol").GetComponent<OyunKontrol>();
        PlayerPrefs.GetInt("HighScoreKayit");
        PlayerPrefs.GetInt("soruSayac");
        txtHighScore.text="High Score = "+ (PlayerPrefs.GetInt("HighScoreKayit")).ToString();
        ses = GetComponent<AudioSource>();
        gameOver = false; //tekrar oynadığımda karakter koşması için
    }

    public void Update()
    {
        if (tekZiplama)
        {
            if (Input.GetMouseButtonDown(0)) //mouse tıklandığında veya telefona tıklayınca
            {
                fizik.AddForce(new Vector2(0, 450));
                ses.clip = ZiplamaSes;
                ses.Play();
                tekZiplama = false;
            }
        
            //if (Input.GetKeyDown(KeyCode.Space)) 
            //{
            //    fizik.AddForce(new Vector2(0, 500));
            //    tekZiplama = false;
            //}
        }
        if (!gameOver) //oyun bitmediyse koşmaya devam et
            KosmaAnim();        
    }
   

    void KosmaAnim()
    {
        kosmaAnimZaman += Time.deltaTime;
        if (tekZiplama) //havada tekrar zıplamamak için 
        {            
                sp_renderer.sprite = kosma[runAnim++];
                if (runAnim == 18) //dizi taşma sorunu için
                    runAnim = 0;                           
        }
        else
        {
            if (fizik.velocity.y > 0)
            {
                sp_renderer.sprite = ziplama[0];
            }
            else
                sp_renderer.sprite = ziplama[1];
        }
    }
 
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "puan")
        {
            puan++;
            txtScore.text = "Score = " + puan;
            ses.clip = PuanSes;
            ses.Play();         
            Time.timeScale += 0.3f; //yer engelden geiçnce oyun hızlanıyor
          
        }
        if (col.gameObject.tag == "engel")
        {
            Time.timeScale = 1f;
            ses.clip = CarpmaSes;
            ses.Play();
            gameOver = true; //Koşma animasyonunu durdurur
            oyunKontrol.gameOver(); //arkaplan ve engelleri durdurur
            highScore = PlayerPrefs.GetInt("HighScoreKayit");
            if (puan > highScore)
            {
                highScore = puan;
                PlayerPrefs.SetInt("HighScoreKayit", highScore);   
            }
            soruCikmaSayac++;
            soruCikmaSayac += PlayerPrefs.GetInt("soruSayac");
            PlayerPrefs.SetInt("soruSayac", soruCikmaSayac); //kaçıncı oyunda olduğu bilgisi hafızaya kaydedildi           
           
            if (soruCikmaSayac == 2)
            { 
                Invoke("SoruSahnesineDon", 2);
                PlayerPrefs.SetInt("soruSayac", 0);
               
            }            
            else
            Invoke("AnaMenuyeDon", 2); //belirtilen saniye sonra belirtilen metodu çalıştır                       
        }
    }

    void AnaMenuyeDon()
    {
        SceneManager.LoadScene("AnaMenu");
    }
    void SoruSahnesineDon()
    {
        SceneManager.LoadScene("SoruSahne");
    }

    private void OnCollisionEnter2D(Collision2D col) //tekrar zıplamak için yere temas etmesi 
    {
        tekZiplama = true;
    }
   
}
