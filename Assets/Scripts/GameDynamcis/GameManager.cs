using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    SpawnerManager spawner;
    BoardManager board;

    private ShapeManager aktifSekil;

    [Header("Sayaçlar")]
    [Range(0.02f,1f)]
    [SerializeField] private float asagiInmeSuresi = .5f;
    private float asagiInmeSayac;
    private float asagiInmeLevelSayac;
    [Range(0.02f,1f)]
    [SerializeField] private float sagSolTusaBasmaSuresi = 0.25f;
    private float sagSolTusaBasmaSayac;
    [Range(0.02f,1f)]
    [SerializeField] private float sagSolDonmeSuresi = 0.25f;
    private float sagSolDonmeSayac;
    [Range(0.02f,1f)]
    [SerializeField] private float asagiTusaBasmaSuresi = 0.25f;
    private float asagiTusaBasmaSayac;

    public bool gameOver = false;

    public bool saatYonumu = true;

    public IconAcKapaManager rotateIcon;

    public GameObject gameOverPanel;

    private ScoreManager scoreManager;

    private TakipShapeManager takipShape;

    private ShapeManager eldekiSekil;

    public Image eldekiSekilImg;

    private bool eldekiDegistirilebilirMi = true;

    private bool hareketEtsinmi = true;

    public ParticleManager[] seviyeAtlamaEfektlerii = new ParticleManager[5];
    public ParticleManager[] gameOverEfektlerii = new ParticleManager[5];

    enum Direction { none, sol, sag, yukari, asagi }

    private Direction suruklemeYonu = Direction.none;
    private Direction suruklemeBitisYonu = Direction.none;

    private float sonrakiDokunmaZamani;
    private float sonrakiSuruklemeZamani;

    [Range(0.05f, 1f)] public float minDokunmaZamani = 0.15f;
    [Range(0.05f, 1f)] public float minSuruklemeZamani = 0.3f;

    private bool dokundumu = false;

    private void Awake()
    {
        board = GameObject.FindAnyObjectByType<BoardManager>();
        spawner = GameObject.FindAnyObjectByType<SpawnerManager>();
        scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
        takipShape = GameObject.FindAnyObjectByType<TakipShapeManager>();
    }

    private void OnEnable()
    {
        TouchManager.DragEvent += SurukleFNC;
        TouchManager.SwipeEvent += SurukleBittiFNC;
        TouchManager.TapEvent += TapFNC;
    }

    private void Osable()
    {
        TouchManager.DragEvent -= SurukleFNC;
        TouchManager.SwipeEvent -= SurukleBittiFNC;
        TouchManager.TapEvent -= TapFNC;
    }

    public void OyunaBaslaFNC()
    {
        if (spawner)
        {
            spawner.HepsiniNullYapFNC();

            if (aktifSekil == null)
            {
                aktifSekil = spawner.SekilOlusturFNC();
                aktifSekil.transform.position = VectoruIntYapFNC(aktifSekil.transform.position);
            }

            if (aktifSekil)
            {
                aktifSekil.transform.localScale = Vector3.zero;
                hareketEtsinmi = false;
                aktifSekil.transform.DOScale(1, .5f).SetEase(Ease.OutBack).OnComplete(() => hareketEtsinmi = true);
            }

            if (eldekiSekil == null)
            {
                eldekiSekil = spawner.EldekiShapeOlusturFNC();

                if (eldekiSekil.name == aktifSekil.name)
                {
                    Destroy(eldekiSekil.gameObject);
                    eldekiSekil = spawner.EldekiShapeOlusturFNC();

                    eldekiSekilImg.sprite = eldekiSekil.shapeSekil;
                    eldekiSekil.gameObject.SetActive(false);
                }
                else
                {
                    eldekiSekilImg.sprite = eldekiSekil.shapeSekil;
                    eldekiSekilImg.GetComponent<CanvasGroup>().alpha = 1f;
                    eldekiSekil.gameObject.SetActive(false);
                }
            }
        }

        if (gameOverPanel)
        {
            gameOverPanel.SetActive(false);
        }

        asagiInmeLevelSayac = asagiInmeSuresi;
    }

    private void Update()
    {
        if (!board || !spawner || !aktifSekil || gameOver || !scoreManager || !hareketEtsinmi)
        {
            return;
        }

        GirisKontrolFNC();
    }

    private void LateUpdate()
    {
        if (!board || !spawner || !aktifSekil || gameOver || !scoreManager || !takipShape || !hareketEtsinmi)
        {
            return;
        }

        if (takipShape)
        {
            takipShape.TakipShapeOlusturFNC(aktifSekil, board);
        }
    }

    void GirisKontrolFNC()
    {
        if ((Input.GetKey("right")) && Time.time > sagSolTusaBasmaSayac || Input.GetKeyDown("right"))
        {
            SagaHareketFNC();
        }
        else if ((Input.GetKey("left")) && Time.time > sagSolTusaBasmaSayac || Input.GetKeyDown("left"))
        {
            SolaHareketFNC();
        }
        else if ((Input.GetKeyDown("up") && Time.time > sagSolDonmeSayac))
        {
            DondurFNC();
        }
        else if (((Input.GetKey("down") && Time.time > asagiTusaBasmaSayac)) || Time.time > asagiInmeSayac)
        {
            AsagiHareketFNC();
        }
        else if ((suruklemeBitisYonu == Direction.sag && Time.time > sonrakiSuruklemeZamani) ||
                (suruklemeYonu == Direction.sag && Time.time > sonrakiDokunmaZamani))
        {
            SagaHareketFNC();
            sonrakiDokunmaZamani = Time.time + minDokunmaZamani;
            sonrakiSuruklemeZamani = Time.time + minSuruklemeZamani;
            // suruklemeYonu = Direction.none;
            // suruklemeBitisYonu = Direction.none;
        }
        else if ((suruklemeBitisYonu == Direction.sol && Time.time > sonrakiSuruklemeZamani) ||
                (suruklemeYonu == Direction.sol && Time.time > sonrakiDokunmaZamani))
        {
            SolaHareketFNC();
            // suruklemeYonu = Direction.none;
            // suruklemeBitisYonu = Direction.none;
        }
        else if ((suruklemeBitisYonu == Direction.yukari && Time.time > sonrakiSuruklemeZamani) || (dokundumu))
        {
            DondurFNC();
            sonrakiSuruklemeZamani = Time.time + minSuruklemeZamani;
            // suruklemeBitisYonu = Direction.none;
        }
        else if (suruklemeYonu == Direction.asagi && Time.time > sonrakiDokunmaZamani)
        {
            AsagiHareketFNC();
            // suruklemeYonu = Direction.none;
        }

        suruklemeYonu = Direction.none;
        suruklemeBitisYonu = Direction.none;
        dokundumu = false;
    }

    private void SagaHareketFNC()
    {
        aktifSekil.SagaHareketFNC();
        sagSolTusaBasmaSayac = Time.time + sagSolTusaBasmaSuresi;

        if (!board.GecerliPozisyondami(aktifSekil))
        {
            SoundManager.instance.SesEfektiCikar(1);
            aktifSekil.SolaHareketFNC();
        }
        else
        {
            SoundManager.instance.SesEfektiCikar(2);
        }
    }

    private void SolaHareketFNC()
    {
        aktifSekil.SolaHareketFNC();
        sagSolTusaBasmaSayac = Time.time + sagSolTusaBasmaSuresi;

        if (!board.GecerliPozisyondami(aktifSekil))
        {
            SoundManager.instance.SesEfektiCikar(1);
            aktifSekil.SagaHareketFNC();
        }
        else
        {
            SoundManager.instance.SesEfektiCikar(2);
        }
    }

    private void DondurFNC()
    {
        aktifSekil.SagaDonFNC();
        sagSolDonmeSayac = Time.time + sagSolDonmeSuresi;

        if (!board.GecerliPozisyondami(aktifSekil))
        {
            SoundManager.instance.SesEfektiCikar(1);
            aktifSekil.SolaDonFNC();
        }
        else
        {
            saatYonumu = !saatYonumu;

            if (rotateIcon)
            {
                rotateIcon.IconAcKapatFNC(saatYonumu);
            }

            SoundManager.instance.SesEfektiCikar(2);
        }
    }
    
    private void AsagiHareketFNC()
    {
        asagiInmeSayac = Time.time + asagiInmeLevelSayac;
        asagiTusaBasmaSayac = Time.time + asagiTusaBasmaSuresi;

        if (aktifSekil)
        {
            aktifSekil.AsagiHareketFNC();

            if (!board.GecerliPozisyondami(aktifSekil))
            {
                if (board.DisariTastimiFNC(aktifSekil))
                {
                    aktifSekil.YukariHareketFNC();
                    gameOver = true;

                    SoundManager.instance.SesEfektiCikar(5);

                    if (gameOverPanel)
                    {
                        StartCoroutine(GameOverRoutineFNC());
                    }
                }
                else
                {
                    YerlestiFNC();
                }
            }
        }
    }

    private void YerlestiFNC()
    {
        if(aktifSekil)
        {
            sagSolTusaBasmaSayac = Time.time;
            asagiTusaBasmaSayac = Time.time;
            sagSolDonmeSayac = Time.time;

            aktifSekil.YukariHareketFNC();

            aktifSekil.YerlesmeEfektiCikarFNC();

            board.SekliIzgaraIcineAlFNC(aktifSekil);
            SoundManager.instance.SesEfektiCikar(4);

            eldekiDegistirilebilirMi = true;

            if (spawner)
            {
                aktifSekil = spawner.SekilOlusturFNC();

                if (aktifSekil)
                {
                    aktifSekil.transform.localScale = Vector3.zero;
                    hareketEtsinmi = false;
                    aktifSekil.transform.DOScale(1, .5f).SetEase(Ease.OutBack).OnComplete(() => hareketEtsinmi = true);
                }

                eldekiSekil = spawner.EldekiShapeOlusturFNC();

                if (eldekiSekil.name == aktifSekil.name)
                {
                    Destroy(eldekiSekil.gameObject);
                    eldekiSekil = spawner.EldekiShapeOlusturFNC();

                    eldekiSekilImg.sprite = eldekiSekil.shapeSekil;
                    eldekiSekil.gameObject.SetActive(false);
                }
                else
                {
                    eldekiSekilImg.sprite = eldekiSekil.shapeSekil;
                    eldekiSekil.gameObject.SetActive(false);
                }
            }

            if (takipShape)
            {
                takipShape.ResetFNC();
            }

            StartCoroutine(board.TumSatirlariTemizleFNC());

            if (board.tamamlananSatir > 0)
            {
                scoreManager.SatirSkoru(board.tamamlananSatir);

                if (scoreManager.levelGecildimi)
                {
                    SoundManager.instance.SesEfektiCikar(6);
                    asagiInmeLevelSayac = asagiInmeSuresi - Mathf.Clamp(((float)scoreManager.level - 1) * .1f, 0.05f, 1f);

                    StartCoroutine(SeviyeGecRoutineFNC());
                }
                else
                {
                    if (board.tamamlananSatir > 1)
                    {
                        SoundManager.instance.VocalSesiCikar();
                    }
                }
                SoundManager.instance.SesEfektiCikar(4);
            }
        }
    }

    Vector2 VectoruIntYapFNC(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    public void RotationIconYonuFNC()
    {
        saatYonumu = !saatYonumu;

        aktifSekil.SaatYonundeDonsun(saatYonumu);

        if (!board.GecerliPozisyondami(aktifSekil))
        {
            aktifSekil.SaatYonundeDonsun(!saatYonumu);
            SoundManager.instance.SesEfektiCikar(2);
        }
        else
        {
            if (rotateIcon)
            {
                rotateIcon.IconAcKapatFNC(saatYonumu);
            }
            SoundManager.instance.SesEfektiCikar(1);
        }
    }

    public void EldekiSekliDegistirFNC()
    {
        if (eldekiDegistirilebilirMi)
        {
            eldekiDegistirilebilirMi = false;

            aktifSekil.gameObject.SetActive(false);
            eldekiSekil.gameObject.SetActive(true);

            eldekiSekil.transform.position = aktifSekil.transform.position;

            aktifSekil = eldekiSekil;
        }

        if (takipShape)
        {
            takipShape.ResetFNC();
        }
    }

    IEnumerator SeviyeGecRoutineFNC()
    {
        yield return new WaitForSeconds(.2f);

        int sayac = 0;

        while (sayac < seviyeAtlamaEfektlerii.Length)
        {
            seviyeAtlamaEfektlerii[sayac].EfectPlayFNC();
            yield return new WaitForSeconds(.3f);

            sayac++;
        }
    }

    IEnumerator GameOverRoutineFNC()
    {
        yield return new WaitForSeconds(.2f);
        int sayac = 0;

        while (sayac < seviyeAtlamaEfektlerii.Length)
        {
            gameOverEfektlerii[sayac].EfectPlayFNC();
            yield return new WaitForSeconds(.1f);

            sayac++;
        }

        yield return new WaitForSeconds(1f);

        if (gameOverPanel)
        {
            gameOverPanel.transform.localScale = Vector3.zero;
            gameOverPanel.SetActive(true);

            gameOverPanel.transform.DOScale(1, .5f).SetEase(Ease.OutBack);
        }
    }

    void SurukleFNC(Vector2 suruklemeHareket)
    {
        suruklemeYonu = YonuBelirleFNC(suruklemeHareket);
    }

    void SurukleBittiFNC(Vector2 suruklemeHareket)
    {
        suruklemeBitisYonu = YonuBelirleFNC(suruklemeHareket);
    }

    void TapFNC(Vector2 suruklemeHareket)
    {
        dokundumu = true;
    }

    Direction YonuBelirleFNC(Vector2 suruklemeHareket)
    {
        Direction suruklemeYonu = Direction.none;

        if (Mathf.Abs(suruklemeHareket.x) > Mathf.Abs(suruklemeHareket.y))
        {
            suruklemeYonu = (suruklemeHareket.x >= 0) ? Direction.sag : Direction.sol;
        }
        else
        {
            suruklemeYonu = (suruklemeHareket.y >= 0) ? Direction.yukari : Direction.asagi;
        }

        return suruklemeYonu;
    }
}
