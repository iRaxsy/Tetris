using System.Collections;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private bool donebilirmi;

    public Sprite shapeSekil;

    public GameObject[] yerlesmeEfektleri;

    // public string efektAdi = "YerlesmeEfekti";

    public void Start()
    {
        yerlesmeEfektleri = GameObject.FindGameObjectsWithTag("YerlesmeEfekti");
    }

    public void YerlesmeEfektiCikarFNC()
    {
        int sayac = 0;
        
        foreach (Transform child in gameObject.transform)
        {
            if (yerlesmeEfektleri[sayac])
            {
                yerlesmeEfektleri[sayac].transform.position = new Vector3(child.position.x, child.position.y, 0f);

                ParticleManager particleManager = yerlesmeEfektleri[sayac].GetComponent<ParticleManager>();

                if (particleManager)
                {
                    particleManager.EfectPlayFNC();
                }
            }

            sayac++;
        }
    }

    public void SolaHareketFNC()
    {
        transform.Translate(Vector3.left, Space.World);
    }
    public void SagaHareketFNC()
    {
        transform.Translate(Vector3.right, Space.World);
    }
    public void AsagiHareketFNC()
    {
        transform.Translate(Vector3.down, Space.World);
    }
    public void YukariHareketFNC()
    {
        transform.Translate(Vector3.up, Space.World);
    }

    public void SagaDonFNC()
    {
        if (donebilirmi)
        {
            transform.Rotate(0, 0, -90);
        }
    }
    public void SolaDonFNC()
    {
        if (donebilirmi)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    IEnumerator HareketRoutine()
    {
        while (true)
        {
            AsagiHareketFNC();
            yield return new WaitForSeconds(.25f);
        }
    }

    public void SaatYonundeDonsun(bool saatYonumu)
    {
        if (saatYonumu)
        {
            SagaDonFNC();
        }
        else
        {
            SolaDonFNC();
        }
    }
}
