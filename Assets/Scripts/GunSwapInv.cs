using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwapInv : MonoBehaviour
{
    public GameObject[] currentGuns;
    public GameObject gunPosition;
    public static GameObject held;

    private Gun gunScript;
    private GameObject prevGun;
    private int[] ammoStash; // CLIP
    private int[] ammoReserves; // BAG

    void Awake()
    {
        Equip(currentGuns[0]);

        ammoStash = new int[currentGuns.Length];
        ammoReserves = new int[currentGuns.Length];
        
        for(int i = 0; i < ammoStash.Length; i++)
        {
            gunScript = currentGuns[i].GetComponent<Gun>();
            ammoStash[i] = gunScript.clipSize;
        }

        for (int i = 0; i < currentGuns.Length; i++)
        {
            gunScript = currentGuns[i].GetComponent<Gun>();
            ammoReserves[i] = gunScript.ammoHeld;
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && held != currentGuns[0])
            {
                Swap(held, currentGuns[0], 0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && held != currentGuns[1])
            {
                Swap(held, currentGuns[1], 1);
            }
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.Log("INVALID NUMBER OF WEAPONS, ARRAY OUT OF BOUND EXCEPTION");
        }
    }

    void Equip(GameObject gun)
    {
        held = Instantiate(gun, gunPosition.transform);
    }

    // Swap guns, handles ammo containment as well (Ammo staying the same between swaps)
    void Swap(GameObject currentHeld, GameObject newGun, int slotNum)
    {
        //STORE AMMO
        Gun script = currentHeld.GetComponent<Gun>();
        ammoStash[(int)script.slot] = script.clipSize;
        ammoReserves[(int)script.slot] = script.ammoHeld;

        Destroy(currentHeld);

        held = Instantiate(newGun, gunPosition.transform);
        
        // REPLACE AMMO
        script = held.GetComponent<Gun>();
        script.clipSize = ammoStash[slotNum];
        script.ammoHeld = ammoReserves[slotNum];
    }

    public static GameObject getHeld()
    {
        return held;
    }
}