using UnityEngine;

//                                      !!! DEV NOTE: HAVE AMMO BE A PART OF THE GUN ITSELF FOR NOW, LATER CREATE AMMO COUNT FOR THE PLAYER MANAGER !!!

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource shotSound;

    public int clipSize;
    public int ammoHeld;
    public int maxClipSize;
    public int maxAmmo;

    [System.Serializable]
        public enum WeaponType
        {
            Auto = 0,
            Semi = 1
        }

    [System.Serializable]
        public enum slotID
        {
            Primary = 0,
            Secondary = 1
        }

    public WeaponType type;
    public slotID slot;

    // Cooldown time between semi automatic shots
    private float cooldown;
    public float shootCooldown;

    void Start()
    {
        cooldown = 0;
        fpsCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;

            // Safety in case computer's internal clock fucks up
            if (cooldown < 0)
            {
                cooldown = 0;
            }
        }

        if (clipSize < 0)
            clipSize = 0;

        // SEMI
        if (Input.GetButtonDown("Fire1") && type == WeaponType.Semi && cooldown == 0 && clipSize != 0)
        {
            Shoot();

            cooldown = shootCooldown;
        }
        else if(Input.GetButton("Fire1") && type == WeaponType.Auto && cooldown == 0 && clipSize != 0)   // AUTO
        {
            Shoot();

            cooldown = shootCooldown;
        }

        if (Input.GetKeyDown("r") && clipSize != maxClipSize)
        {
            Reload();
        }
    }

    // Raycast shooting system, Projectile weapons will have a seperate script entirely
    void Shoot()
    {
        RaycastHit hit;

        muzzleFlash.Play();

        shotSound.Play();

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

            // Impact effect upon hitting a surface, readd when Particle System is created for it
            /*
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            */
        }

        clipSize--;
    }

    void Reload()
    {
        if(ammoHeld > maxClipSize)
        {
            //!!!   ANIMATION LATER     !!!
            while(ammoHeld > 0 && clipSize < maxClipSize)
            {
                clipSize++;
                ammoHeld--;
            }
        }
    }
}
