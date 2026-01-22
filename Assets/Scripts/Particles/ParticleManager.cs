using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem[] tumEfektler;

    private void Start()
    {
        tumEfektler = GetComponentsInChildren<ParticleSystem>();
    }

    public void EfectPlayFNC()
    {
        foreach (ParticleSystem efect in tumEfektler)
        {
            efect.Stop();
            efect.Play();
        }
    }
}
