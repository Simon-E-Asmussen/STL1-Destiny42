using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reposIndicator : MonoBehaviour
{

    public GameObject HANS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReposIndicator(Vector3 cringeLordOverturn)
    {
        this.transform.position = cringeLordOverturn;
    }

    public void SweetRelease()
    {
        Debug.Log("Kill this indicator!");
        Destroy(HANS);
    }
}
