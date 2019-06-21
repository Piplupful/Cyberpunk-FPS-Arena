using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource shotSound;

    [System.Serializable]
        public enum WeaponType
        {
            Auto = 0,
            Semi = 1
        }

    public WeaponType type;

    // Cooldown time between semi automatic shots
    private float cooldown;
    public float semiShootCooldown;

    void Start()
    {
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            Debug.Log(cooldown + " ---- cooldown time");

            // Safety in case computer's internal clock fucks up
            if (cooldown < 0)
            {
                cooldown = 0;
            }
        }

        if (Input.GetButtonDown("Fire1") && type == WeaponType.Semi && cooldown == 0)
        {
            Shoot();

            cooldown = semiShootCooldown;
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
            Debug.Log(hit.transform.name);

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
    }
}
