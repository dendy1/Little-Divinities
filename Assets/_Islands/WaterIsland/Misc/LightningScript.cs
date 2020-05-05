using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{

    [SerializeField] GameObject l1;
    [SerializeField] GameObject l2;



    IEnumerator Lightning()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 2; i++)
        {
            l1.SetActive(!l1.active);
            l2.SetActive(!l2.active);
            yield return new WaitForSeconds(.1f);
        }
        StartCoroutine(Lightning());

    }

    // Start is called before the first frame update
    void Start()
    {
        l1.SetActive(false);
        l2.SetActive(false);

        StartCoroutine(Lightning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
