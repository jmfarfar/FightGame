using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHandler : MonoBehaviour
{


    [SerializeField] private GameObject magicLogic;
    
    public void EnableMagic()
    {
        magicLogic.SetActive(true);
    }

    public void DisableMagic()
    {
        magicLogic.SetActive(false);
    }

}
