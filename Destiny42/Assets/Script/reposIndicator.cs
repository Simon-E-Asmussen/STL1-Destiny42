using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reposIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReposIndicator(Transform cringeLordOverturn)
    {
        this.transform.position = cringeLordOverturn.position;
    }

    public void SweetRelease()
    {
        GameObject.Destroy(this);
    }
}
