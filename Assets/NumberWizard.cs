using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour {
    int max, min,oncekiTahmin,tahmin,tahminSayisi;
    bool maxSecildi=false, minSecildi=false;

    enum oyunDurumlari
    {
        Baslamadi,
        Basladi,
        Hileci
    };
    oyunDurumlari oyunDurumu = oyunDurumlari.Baslamadi;

	// Use this for initialization
	void Start () {
        tahminSayisi = 0;
        
        Debug.Log("Sayi sihirbazina hosgeldin .");
        Debug.Log("Bu oyunda senden belli bir aralıkta bir sayı tutmanı isteyeceğim ve bu sayıyı bilmeye çalışacağım .");
        Debug.Log("Ama önce sayı aralığının en küçük sayısını klavyenden girip ENTER tuşuna bas .");
	}

    void SayiyiBelirle(string sayi,bool minMi)
    {
        if(minMi)
        {
            string tempMin = min.ToString();
            tempMin += sayi;
            min = int.Parse(tempMin);
            Debug.Log(min);
        }
        else
        {
            string tempMax = max.ToString();
            tempMax += sayi;
            max = int.Parse(tempMax);
            Debug.Log(max);
        }
    }
    
    void SonHaneyiSil(bool minMi)
    {
        if (minMi)
        {
            min /= 10;
            Debug.Log(min);
        }
        else
        {
            max /= 10;
            Debug.Log(max);
        }
    }

    int RastgeleTahmin()
    {
        return Random.Range(min, max);
    }

	void SonrakiTahmin()
    {
        tahmin = RastgeleTahmin();
        Debug.Log("Tuttuğun sayı " + tahmin + "den büyükse YUKARI OK, küçükse AŞAĞI OK tuşuna bas. Tahminim doğruysa ENTER tuşuna bas .");
        tahminSayisi++;
    }

	// Update is called once per frame
	void Update () {

        if (oyunDurumu == oyunDurumlari.Basladi&&Input.anyKeyDown)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                oncekiTahmin = tahmin;
                min = tahmin;
                SonrakiTahmin();
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                oncekiTahmin = tahmin;
                max = tahmin;
                SonrakiTahmin();
            }
            else if(Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Tuttuğun sayıyı "+tahminSayisi+" denemede bildim !");
                UnityEditor.EditorApplication.isPlaying = false;
            }
            if (oncekiTahmin == min&&oncekiTahmin==max)
                oyunDurumu=oyunDurumlari.Hileci;              
        }
        else if (oyunDurumu == oyunDurumlari.Hileci)
        {
            Debug.Log("HİLECİ !");
        }

        if (Input.anyKeyDown&&oyunDurumu==oyunDurumlari.Baslamadi)
        {
            string key = Input.inputString;
            int x;
            bool isNumeric = int.TryParse(key,out x);

            if (isNumeric && (!minSecildi||!maxSecildi))
            {
                SayiyiBelirle(key, !minSecildi);
            }

            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if(!minSecildi)
                {
                    Debug.Log("Minimum sayıyı seçtiniz.");
                    Debug.Log("Şimdi de maksimum sayıyı giriniz.");
                    minSecildi = true;
                }
                else if(!maxSecildi)
                {
                    if(min>max)
                    {
                        Debug.Log("Minimum sayınız maksimum sayınızdan büyük olmalı . Lütfen sayınızı tekrar yazınız ");
                        max = 0;
                    }
                    else
                    {
                        Debug.Log("Maksimum sayıyı seçtiniz. Tahminlerime başlıyorum.");
                        maxSecildi = true;
                        oyunDurumu = oyunDurumlari.Basladi;
                        SonrakiTahmin();
                    }                   
                }               
            }
                
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SonHaneyiSil(!minSecildi);
            }
               
        }
	}
}
