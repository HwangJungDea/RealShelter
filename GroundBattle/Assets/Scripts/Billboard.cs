using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//메인 카메라의 회전방향과 일치시키고 싶다. 계속.
public class Billboard : MonoBehaviour
{
    Transform mainCamera; //캐싱이라한다.
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
