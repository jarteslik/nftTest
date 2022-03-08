using System.Collections;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    bool mouseOn;
    float rotationSpeed = 5;
    private void OnMouseDown()
    {
        mouseOn = true;
        StartCoroutine(CheckRotate());
    }

    private void OnMouseUp()
    {
        mouseOn = false;
    }
    
    IEnumerator CheckRotate() 
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();
        Vector3 rotation = Vector3.zero;
        while (mouseOn)
        {
            yield return delay;
            rotation.x = Input.GetAxis("Mouse Y") * rotationSpeed;
            rotation.y = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.Rotate(rotation, Space.World);
        }
    }

}
