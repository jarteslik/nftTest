using UnityEngine;
using UnityEngine.UI;

public class LocalizedImage : MonoBehaviour
{
    [SerializeField]
    Sprite ruImg, engImg;
    [SerializeField]
    Image currentImage;

    private void Start()
    {
        currentImage.sprite = LocalizationController.Instance.CurrentLanguage == "RUS" ? ruImg : engImg;
    }

}
