using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] ShapeManager[] tumSekiller;

    [SerializeField] private Image[] sekilImages = new Image[2];

    private ShapeManager[] siradakiSekiller = new ShapeManager[2];

    public ShapeManager SekilOlusturFNC()
    {
        ShapeManager sekil = null;

        sekil = SiradakiSekliAlFNC();
        sekil.gameObject.SetActive(true);
        sekil.transform.position = transform.position;

        if (sekil != null)
        {
            return sekil;
        }
        else
        {
            print("Dizi Boş!");
            return null;
        }
    }

    public void HepsiniNullYapFNC()
    {
        for (int i = 0; i < siradakiSekiller.Length; i++)
        {
            siradakiSekiller[i] = null;
        }

        SirayiDoldurFNC();
    }

    void SirayiDoldurFNC()
    {
        for (int i = 0; i < siradakiSekiller.Length; i++)
        {
            if (!siradakiSekiller[i])
            {
                siradakiSekiller[i] = Instantiate(RastgeleSekilOlusturFNC(), transform.position, Quaternion.identity) as ShapeManager;
                siradakiSekiller[i].gameObject.SetActive(false);
                sekilImages[i].sprite = siradakiSekiller[i].shapeSekil;
            }
        }

        StartCoroutine(SekilImageAcRoutine());
    }

    IEnumerator SekilImageAcRoutine()
    {
        for (int i = 0; i < sekilImages.Length; i++)
        {
            sekilImages[i].GetComponent<CanvasGroup>().alpha = 0f;
            sekilImages[i].GetComponent<RectTransform>().localScale = Vector3.zero;
        }

        yield return new WaitForSeconds(.1f);

        int sayac = 0;

        while (sayac < sekilImages.Length)
        {
            sekilImages[sayac].GetComponent<CanvasGroup>().DOFade(1, .6f);
            sekilImages[sayac].GetComponent<RectTransform>().DOScale(1, .6f).SetEase(Ease.OutBack);
            sayac++;
            yield return new WaitForSeconds(.4f);
        }
    }

    ShapeManager RastgeleSekilOlusturFNC()
    {
        int randomSekil = Random.Range(0, tumSekiller.Length);

        if (tumSekiller[randomSekil])
        {
            return tumSekiller[randomSekil];
        }
        else
        {
            return null;
        }
    }

    ShapeManager SiradakiSekliAlFNC()
    {
        ShapeManager sonrakiSekil = null;

        if (siradakiSekiller[0])
        {
            sonrakiSekil = siradakiSekiller[0];
        }

        for (int i = 1; i < siradakiSekiller.Length; i++)
        {
            siradakiSekiller[i - 1] = siradakiSekiller[i];
            sekilImages[i - 1].sprite = siradakiSekiller[i - 1].shapeSekil;
        }

        siradakiSekiller[siradakiSekiller.Length - 1] = null;

        SirayiDoldurFNC();

        return sonrakiSekil;
    }

    public ShapeManager EldekiShapeOlusturFNC()
    {
        ShapeManager eldekiSekil = null;

        eldekiSekil = Instantiate(RastgeleSekilOlusturFNC(), transform.position, Quaternion.identity) as ShapeManager;
        eldekiSekil.transform.position = transform.position;
        return eldekiSekil;
    }
}
