using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Vector3 camOffset = new Vector3();
    public Camera _cam;
    private Transform folObjTransform;
    // Start is called before the first frame update
    void Start()
    {
        folObjTransform = GameObject.Find("cat").GetComponent<Transform>();
        _cam = GetComponent<Camera>(); 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _cam.transform.position = folObjTransform.transform.position + camOffset;
    }
    
}
