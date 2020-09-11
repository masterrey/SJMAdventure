using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponControl : MonoBehaviour
{
    public VisualEffect vfx;
    public Animator anim;
    public AudioSource aud;
    public Light light;
    public VisualEffect vfximpact;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());

        }
        
    }

    IEnumerator Shoot()
    {
        anim.SetBool("Shoot", true);

        yield return new WaitForSeconds(0.2f);

        vfx.Play();
        aud.pitch=Random.Range(1.20f, 2f);
        aud.Play();
        light.intensity = 5;
        if (Physics.Raycast(vfx.transform.position, vfx.transform.forward, out RaycastHit hit, 100))
        {
            vfximpact.transform.position = hit.point;
            Rigidbody rdb = hit.collider.gameObject.GetComponent<Rigidbody>();
            if (rdb)
            {
                //rdb.AddForce(vfx.transform.forward * 10, ForceMode.Impulse);
                rdb.AddForceAtPosition(vfx.transform.forward * 10, hit.point, ForceMode.Impulse);
            }
        }

        yield return new WaitForSeconds(0.01f);
        vfximpact.Play();
        light.intensity = 0;
        anim.SetBool("Shoot", false);
    }
}
