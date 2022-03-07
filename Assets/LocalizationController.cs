using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationController : MonoBehaviour
{
    public string CurrentLanguage;

    public static Dictionary<string, string> RusWords = new Dictionary<string, string>()
    {
        {"OWN", "ВЛАДЕЛЕЦ"},
        {"CRT", "СОЗДАТЕЛЬ"},
        {"CLCT", "КОЛЛЕКЦИЯ"},
        {"SRCH", "ПОИСК"},
        {"ADR", "ВВЕДИТЕ АДРЕС:"},
        {"DWNLD", "ЗАГРУЗКА ИЗОБРАЖЕНИЙ"},
        {"ERR", "ЧТО ТО ПОШЛО НЕ ТАК"}
    };

    public static Dictionary<string, string> EngWords = new Dictionary<string, string>()
    {
        {"OWN", "OWNER"},
        {"CRT", "CREATOR"},
        {"CLCT", "COLLECTION"},
        {"SRCH", "SEARCH"},
        {"ADR", "ENTER ADDRES:"},
        {"DWNLD", "DOWNLOAD IMAGES"},
        {"ERR", "SOMETHING GONE WRONG"}
    };

    private void Awake()
    {
        CurrentLanguage = Application.systemLanguage == SystemLanguage.Russian ? "RUS" : "ENG";
    }

    static LocalizationController instance;
    public static LocalizationController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LocalizationController>();
            }
            return instance;
        }
    }

}
