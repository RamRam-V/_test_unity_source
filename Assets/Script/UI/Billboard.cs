using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(gameObject.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
