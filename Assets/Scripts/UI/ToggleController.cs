using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public string SourceName;
    [SerializeField]
    private Image btnImage;
    [SerializeField]
    private Text btnText;
    [SerializeField]
    Color toggleOn, ToggleOff;

    public void Choose(bool toggle)
    {
        btnImage.color = new Color(1, 1, 1, toggle ? 1 : 0);
        btnText.color = toggle ? toggleOn : ToggleOff;
    }
    
}
