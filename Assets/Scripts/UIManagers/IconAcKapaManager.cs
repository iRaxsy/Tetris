using UnityEngine;
using UnityEngine.UI;

public class IconAcKapaManager : MonoBehaviour
{
    public Sprite acikIcon;
    public Sprite kapaliIcon;

    private Image iconImg;

    public bool varsayilanIconDurumu = true;

    private void Start()
    {
        iconImg = GetComponent<Image>();

        iconImg.sprite = (varsayilanIconDurumu) ? acikIcon : kapaliIcon;
    }

    public void IconAcKapatFNC(bool iconDurumu)
    {
        if (!iconImg || !acikIcon || !kapaliIcon)
        {
            return;
        }
        iconImg.sprite = (iconDurumu) ? acikIcon : kapaliIcon;
    }
}
