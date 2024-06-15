using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(this.GetComponent<PlayerId>().playerId).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
