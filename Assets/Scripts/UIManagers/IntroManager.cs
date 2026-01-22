using UnityEngine;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;

public class IntroManager : MonoBehaviour
{
    public GameObject[] sayilar;

    public GameObject sayilarTransform;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(SayilariAcRoutine());
    }

    IEnumerator SayilariAcRoutine()
    {
        yield return new WaitForSeconds(.1f);
        sayilarTransform.GetComponent<RectTransform>().DORotate(Vector3.zero, .3f).SetEase(Ease.OutBack);
        sayilarTransform.GetComponent<CanvasGroup>().DOFade(1, .3f);

        yield return new WaitForSeconds(.2f);

        int sayac = 0;

        while (sayac < sayilar.Length)
        {
            sayilar[sayac].GetComponent<RectTransform>().DOLocalMoveY(0, .5f);
            sayilar[sayac].GetComponent<CanvasGroup>().DOFade(1, .5f);

            sayilar[sayac].GetComponent<RectTransform>().DOScale(2f, .3f).SetEase(Ease.OutBounce).OnComplete(() =>
                sayilar[sayac].GetComponent<RectTransform>().DOScale(1f, .3f).SetEase(Ease.InBack));

            yield return new WaitForSeconds(1.5f);

            sayac++;

            sayilar[sayac - 1].GetComponent<RectTransform>().DOLocalMoveY(150f, .5f);

            yield return new WaitForSeconds(.1f);
        }

        sayilarTransform.GetComponent<CanvasGroup>().DOFade(0, .5f).OnComplete(() =>
            {
                sayilarTransform.transform.parent.gameObject.SetActive(false);
                // Oyun Başlatma
                gameManager.OyunaBaslaFNC();
            }
        );
    }
}
