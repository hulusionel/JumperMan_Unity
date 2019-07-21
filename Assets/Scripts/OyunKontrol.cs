using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunKontrol : MonoBehaviour
{

    public GameObject gokyuzu1;
    public GameObject gokyuzu2;
    public GameObject engel;
    GameObject[] engelDizi;

    Rigidbody2D fizik1;
    Rigidbody2D fizik2;
    Rigidbody2D fizikEngel;

    public float sahneHizi = -3f; 
    float gokyuzuUzunluk = 0;    //arkadaki sahne öne geçmesi için
    float engelDegisimZaman = 0; //
    public int engelSayisi = 5;
    int sayac = 0;
    bool pause = false;

    void Start()
    {
        fizik1 = gokyuzu1.GetComponent<Rigidbody2D>();
        fizik2 = gokyuzu2.GetComponent<Rigidbody2D>();
        engelDizi = new GameObject[engelSayisi]; //oyun panelinden erişim ile engel sayısı artırılıp azaltılabilir

        ArkaplanHareket(sahneHizi); 
        gokyuzuUzunluk = gokyuzu1.GetComponent<BoxCollider2D>().size.x;

        //Engeli oluşturma ve sahne hızında hareket ettirme
        for (int i = 0; i < engelDizi.Length; i++)
        {
            engelDizi[i] = Instantiate(engel, new Vector2(-20, -20), Quaternion.identity);//sahnede görünmeyen bir yerde engeller oluşturuldu
            //rigitbody özelliğini kod olarak ekledim
            fizikEngel = engelDizi[i].AddComponent<Rigidbody2D>();
            fizikEngel.gravityScale = 0;
            fizikEngel.velocity = new Vector2(sahneHizi, 0);

        }       
    }
           
    void Update()
    {
        if (gokyuzu1.transform.position.x <= -gokyuzuUzunluk)
            gokyuzu1.transform.position += new Vector3(gokyuzuUzunluk * 2, 0);

        if (gokyuzu2.transform.position.x <= -gokyuzuUzunluk)
            gokyuzu2.transform.position += new Vector3(gokyuzuUzunluk * 2, 0);
       
        //belli sayıdaki engelin tekrar oluşması
        engelDegisimZaman += Time.deltaTime;
       
        float engelCikmaSuresi = 3f;

        if (engelDegisimZaman > engelCikmaSuresi)
        {
            engelDegisimZaman = 0;
            engelDizi[sayac].transform.position = new Vector3(10, -3.4f);
            sayac++;
            if (sayac >= engelDizi.Length)
            {
                sayac = 0;
            }
        }
    }

    public void gameOver()
    {
        //Engeller ve arkaplanlar durduruldu
        for (int i = 0; i < engelDizi.Length; i++)
        {
            engelDizi[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;//yukarda dizi yaparak bellekte oluşturmadığım için tekrar verdim           
            ArkaplanHareket(0);
        }
    }

    public void ArkaplanHareket(float hiz) //farklı yerlerden erişim ile tek fonksiyon ile farklı işler
    {
        fizik1.velocity = new Vector2(hiz, 0);
        fizik2.velocity = new Vector2(hiz, 0);

       
    }
   
    public void Pause()
    {
        //Engeller ve arkaplanlar durduruldu
        if(!pause)
        {            
           Time.timeScale = 0f;                     
            pause = true;                   
        }
        else if (pause)
        {
            Time.timeScale = 1.0f;
            pause = false;
        }
    }
}
