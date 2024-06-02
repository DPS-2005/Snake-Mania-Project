using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiralmoment : MonoBehaviour

{
    [HideInInspector]
    public MeshRenderer meshRenderer;
    public GameObject Head;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        if(Head.transform.position == transform.position){
            meshRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 90 * Time.deltaTime);
    }
}
