using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    public Text currentClip;
    public Text currentAmmoCount;

    private GameObject heldGun;
    private Gun gunScript;

    void Start()
    {
        heldGun = GunSwapInv.getHeld();
        gunScript = heldGun.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if(heldGun != GunSwapInv.getHeld())
        {
            heldGun = GunSwapInv.getHeld();
            gunScript = heldGun.GetComponent<Gun>();
        }

        currentClip.text = gunScript.clipSize.ToString();
        currentAmmoCount.text = gunScript.ammoHeld.ToString();
    }

    
}
