using UnityEngine;
using TMPro;

public class TouchManager : MonoBehaviour
{
    public delegate void DokunmaEventDelegate(Vector2 swipePos);
    public static event DokunmaEventDelegate DragEvent;
    public static event DokunmaEventDelegate SwipeEvent;
    public static event DokunmaEventDelegate TapEvent;

    private Vector2 dokunmaHareketi;

    [Range(50, 250)] public int minDragUzaklik = 100;

    [Range(20, 250)]
    public int minSuruklemeUzaklik = 50;

    public bool taniKullanilsinmi = false;

    private float tiklamaMaxSure = 0f;

    public float ekranaTiklamaSuresi = .1f;
    
    void TiklandiFNC()
    {
        if (TapEvent != null)
        {
            TapEvent(dokunmaHareketi);
        }
    }

    void surukleFNC()
    {
        if (DragEvent != null)
        {
            DragEvent(dokunmaHareketi);
        }
    }

    void SurukleBittiFNC()
    {
        if (SwipeEvent != null)
        {
            SwipeEvent(dokunmaHareketi);
        }
    }

    string SuruklemeTaniFNC(Vector2 surukleHareket)
    {
        string direction = "";

        if (Mathf.Abs(surukleHareket.x) > Mathf.Abs(surukleHareket.y))
        {
            direction = (surukleHareket.x >= 0) ? "sag" : "sol";
        }
        else
        {
            direction = (surukleHareket.y >= 0 ? "yukarı" : "asagi");
        }

        return direction;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                dokunmaHareketi = Vector2.zero;
                tiklamaMaxSure = Time.time + ekranaTiklamaSuresi;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                dokunmaHareketi += touch.deltaPosition;

                if (dokunmaHareketi.magnitude > minDragUzaklik)
                {
                    surukleFNC();
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (dokunmaHareketi.magnitude > minSuruklemeUzaklik)
                {
                    SurukleBittiFNC();
                }
                else if (Time.time < tiklamaMaxSure)
                {
                    TiklandiFNC();
                }
            }
        }
    }
}
