using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] AudioSource[] sesEfektleri;
    [SerializeField] AudioSource[] vocalClips;

    private AudioClip rastgeleMusicClip;

    public bool musicCalsinmi = true;
    public bool efektCalsinmi = true;

    public IconAcKapaManager musicIcon;
    public IconAcKapaManager fxIcon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rastgeleMusicClip = RastgeleClipSec(musicClips);
        BackgroundMusicCal(rastgeleMusicClip);
    }

    public void VocalSesiCikar()
    {
        if (efektCalsinmi)
        {
            AudioSource source = vocalClips[Random.Range(0, vocalClips.Length)];

            source.Stop();
            source.Play();
        }
    }

    public void SesEfektiCikar(int hangiSes)
    {
        if (efektCalsinmi && hangiSes < sesEfektleri.Length)
        {
            sesEfektleri[hangiSes].volume = PlayerPrefs.GetFloat("FxVolume");
            sesEfektleri[hangiSes].Stop();
            sesEfektleri[hangiSes].Play();
        }
    }

    AudioClip RastgeleClipSec(AudioClip[] clips)
    {
        AudioClip rastgeleClip = clips[Random.Range(0, clips.Length)];
        return rastgeleClip;
    }

    public void BackgroundMusicCal(AudioClip musicClip)
    {
        if (!musicClip || !musicSource || !musicCalsinmi)
        {
            return;
        }

        musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    void MusicGuncelleFNC()
    {
        if (musicSource.isPlaying != musicCalsinmi)
        {
            if (musicCalsinmi)
            {
                rastgeleMusicClip = RastgeleClipSec(musicClips);
                BackgroundMusicCal(rastgeleMusicClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }

    public void MusicAcKapaFNC()
    {
        musicCalsinmi = !musicCalsinmi;
        MusicGuncelleFNC();
        musicIcon.IconAcKapatFNC(musicCalsinmi);
    }

    public void FXAcKapatFNC()
    {
        efektCalsinmi = !efektCalsinmi;

        fxIcon.IconAcKapatFNC(efektCalsinmi);
    }
}
