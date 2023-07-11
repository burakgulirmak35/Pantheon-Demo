using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    private IEnumerator GenerateResourceCoro()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
        }
    }

}
