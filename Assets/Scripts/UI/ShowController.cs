using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowController : MonoBehaviour
{
    [SerializeField]
    List<ToggleController> btns;
    string viewName;
    [SerializeField]
    GameObject viewImg, viewCube;
    [SerializeField]
    MeshRenderer viewCubeRenderer;
    public void ChooseView(ToggleController choosedBtn)
    {
        foreach (ToggleController btn in btns)
        {
            if (btn == choosedBtn)
            {
                btn.Choose(true);
                viewName = btn.SourceName;
            }
            else
            {
                btn.Choose(false);
            }
        }
        switch (viewName)
        {
            case ("2DView"):
                StartCoroutine(ChangeView(viewCube, viewImg));
                break;
            case ("3DView"):
                StartCoroutine(ChangeView(viewImg, viewCube));
                break;
        }
    }

    public void ChooseTexture(Image choosedTexture)
    {
        Texture2D _tex = choosedTexture.sprite.texture;
        viewCubeRenderer.material.mainTexture = _tex;
        
    }

    IEnumerator ChangeView(GameObject currentView, GameObject nextView)
    {
        float time = 0;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.01f);
        Vector3 startPos = currentView.transform.position;
        Vector3 endPos = nextView.transform.position;
        while (time < 1)
        {
            yield return delay;
            time += 0.025f;
            currentView.transform.position = Vector3.Lerp(startPos, endPos, time);
            nextView.transform.position = Vector3.Lerp(endPos, startPos, time);
        }
    }
    
}
