using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Muzzle")]
    [SerializeField]
    private GameObject muzzle;
    [SerializeField]
    private GameObject bullet;

    [Header("Shooting")]
    [SerializeField]
    private float delay;
    private float curDelay;
    [SerializeField]
    private AudioSource audioFx;
    [SerializeField]
    private float impulseForce;
    [Header("Flashlight")]
    [SerializeField]
    private GameObject flashlightObject;
    [SerializeField]
    private bool isFlashlightOn;

    public static bool IsStunned { get; private set; }

    void Start()
    {
        curDelay = delay;
    }

    void Update()
    {
        IsStunned = curDelay < delay;

        curDelay += Time.deltaTime;
        if (curDelay >= delay && Input.GetMouseButton(0))
        {
            audioFx.Stop();

            GameObject bulletSpawned = Instantiate(bullet, muzzle.transform.position, Quaternion.identity);

            audioFx.Play();

            ImpulseBullet(bulletSpawned);

            StartCoroutine(DestroyBullet(bulletSpawned));

            curDelay = 0;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashlightOn = !isFlashlightOn;
            flashlightObject.SetActive(isFlashlightOn);
        }
    }

    private void ImpulseBullet(GameObject bulletGo)
    {
        Rigidbody rb = bulletGo.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * impulseForce, ForceMode.Impulse);
    }

    private IEnumerator DestroyBullet(GameObject bulletGO)
    {
        yield return new WaitForSeconds(10);
        Destroy(bulletGO);
    }
}
