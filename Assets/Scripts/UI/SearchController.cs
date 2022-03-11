using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
public class SearchController : MonoBehaviour
{
    string searchId;
    [SerializeField]
    InputField idField;
    List<string> imgUrls;
    [SerializeField]
    List<ToggleController> btns;
    string source = "Collection";
    
    [SerializeField] 
    private Image[] nftArts;

    public void ChooseSource(ToggleController choosedBtn)
    {
        foreach (ToggleController btn in btns)
        {
            if (btn == choosedBtn) 
            {
                btn.Choose(true);
                source = btn.SourceName;
            }
            else 
            {
                btn.Choose(false);
            }
        }
    }
    public void Search()
    {
        searchId = idField.text;
        StartCoroutine(GetRequest("https://ethereum-api.rarible.org/v0.1/nft/items/by" + source+ "?" + source.ToLower() + "=" + searchId));
    }
    IEnumerator GetRequest(string url)
    {
        imgUrls = new List<string>();
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                //Debug.Log(": Error: " + webRequest.error);
                ScreenController.Instance.ShowPopup(true, "ERR");
            }
            else
            {
                dynamic dynObj = JsonConvert.DeserializeObject(webRequest.downloadHandler.text);
                
                foreach (var data in dynObj.items)
                {
                    if (data.meta.image != null)
                    {
                        //некрасивая строчка, но мета почему то иногда не содержит того или другого
                        string _typeMeta = _typeMeta = (data.meta.image.meta.PREVIEW != null) ? data.meta.image.meta.PREVIEW.type : data.meta.image.meta.ORIGINAL.type; 
                            if (_typeMeta.Contains("png") || _typeMeta.Contains("jpg"))
                            {
                                string _imgUrl = data.meta.image.url.ORIGINAL;
                                imgUrls.Add(_imgUrl);
                            }
                    }
                }
                StartCoroutine(DownloadImages());
            }
        }
    }
    IEnumerator DownloadImages()
    {
        UnityWebRequest webRequestimg = null;

        ScreenController.Instance.ShowPopup(true, "DWNLD");
        for (int i = 0; i < nftArts.Length; i++)
        {
            if (i < imgUrls.Count)
            {
                webRequestimg = UnityWebRequestTexture.GetTexture(imgUrls[i]);
                yield return webRequestimg.SendWebRequest();
                if (webRequestimg.isNetworkError)
                {
                    //Debug.Log("error");
                    ScreenController.Instance.ShowPopup(true, "ERR");
                    StopCoroutine(DownloadImages());
                    break;
                    
                }
                else
                {
                    Texture2D tex = ((DownloadHandlerTexture)webRequestimg.downloadHandler).texture;
                    Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                    nftArts[i].sprite = sprite;
                }
            }
            
        }
        ScreenController.Instance.ShowPopup(false,"DWNLD");
        yield return new WaitForSecondsRealtime(1f);
        ScreenController.Instance.ShowScreen(GameObject.Find("ShowingScreen"));
    }

 }
