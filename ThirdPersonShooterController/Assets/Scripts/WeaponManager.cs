using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    AimStateManager aim;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;

    WeaponAmmo ammo;
    public TMP_Text currentAmmoText;
    public TMP_Text extraAmmoText;

    ActionStateManager actions;

    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponentInParent<AimStateManager>();
        audioSource = GetComponent<AudioSource>();
        ammo = GetComponent<WeaponAmmo>();
        actions = GetComponentInParent<ActionStateManager>();

        fireRateTimer = fireRate;
        UpdateUI(ammo.currentAmmo, ammo.extraAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire()) Fire();
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0) return false;
        if (actions.currentState == actions.Reload) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        audioSource.PlayOneShot(gunShot);
        ammo.currentAmmo--;
        for (int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
        UpdateUI(ammo.currentAmmo, ammo.extraAmmo);
    }

    public void UpdateUI(int currentAmmo, int extraAmmo)
    {
        currentAmmoText.text = currentAmmo.ToString();
        extraAmmoText.text = extraAmmo.ToString();
    }
}
