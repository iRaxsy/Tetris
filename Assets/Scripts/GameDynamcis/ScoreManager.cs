using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int skor = 0;
    private int satirlar;
    public int level = 1;

    public int seviyedekiSatirSayisi = 5;

    private int minSatir = 1;
    private int maxSatir = 4;

    public TextMeshProUGUI satirTxt;
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI skorTxt;

    public bool levelGecildimi = false;

    void Start()
    {
        ResetFNC();
    }

    public void ResetFNC()
    {
        level = 1;
        satirlar = seviyedekiSatirSayisi * level;
        TextGuncelleFNC();
    }

    public void SatirSkoru(int n)
    {
        levelGecildimi = false;
        n = Mathf.Clamp(n, minSatir, maxSatir);

        switch (n)
        {
            case 1:
                skor += 30 * level;
                break;

            case 2:
                skor += 50 * level;
                break;

            case 3:
                skor += 150 * level;
                break;

            case 4:
                skor += 500 * level;
                break;
        }
        satirlar -= n;

        if (satirlar <= 0)
        {
            LevelAtlaFNC();
        }

        TextGuncelleFNC();
    }

    void TextGuncelleFNC()
    {
        if (skorTxt)
        {
            skorTxt.text = BasaSifirEkleFNC(skor, 5);
        }
        if (levelTxt)
        {
            levelTxt.text = level.ToString();
        }
        if (satirTxt)
        {
            satirTxt.text = satirlar.ToString();
        }
    }

    string BasaSifirEkleFNC(int skor, int rakamSayisi)
    {
        string skorStr = skor.ToString();

        while (skorStr.Length < rakamSayisi)
        {
            skorStr = "0" + skorStr;
        }
        return skorStr;
    }
    
    public void LevelAtlaFNC()
    {
        level++;
        satirlar = seviyedekiSatirSayisi * level;
        levelGecildimi = true;
    }
}
