using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    /*-----------------------------------
     * Script used to keep objects alive through multiple scenes
     * ---------------------------------*/

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
