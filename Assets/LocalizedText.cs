using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    string key;
    [SerializeField]
    Text currentText;

    private void Start()
    {
       currentText.text = LocalizationController.Instance.CurrentLanguage == "RUS" ? LocalizationController.RusWords[key] : LocalizationController.EngWords[key];
    }

}
