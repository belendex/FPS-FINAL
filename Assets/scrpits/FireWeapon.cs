using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FIRETYPE { REPEATER, SEMIAUTOMATIC, AUTOMATIC}
public class FireWeapon : MonoBehaviour
{
    [System.Serializable]
    public struct FireWeaponData
    {

        public FIRETYPE firetype;
        public float power;
        public float recoil;
        public float fireRate;
        public float range;
        public int magazineCapacity;
        [Range(0, 1f)]
        public float muzzleFireFrequency;
        public int currentAmmo { get; set; } //Que significa?
        

        public FireWeaponData(FIRETYPE firetype, float power, float recoil, float fireRate, float range, int magazineCapacity, float muzzleFireFrequency)
        {
            this.firetype = firetype;
            this.power = power;
            this.recoil = recoil;
            this.fireRate = fireRate;
            this.range = range;
            this.magazineCapacity = magazineCapacity;
            this.muzzleFireFrequency = muzzleFireFrequency;
            currentAmmo = magazineCapacity;




        }



    }

    [System.Serializable]
    public struct FireWeaponFXData
    {

        public ParticleSystem weaponFireParticles;
        public Light weaponFireLight;
        public AudioClip reloadSound;
        public AudioClip shootSound;
        public AudioClip emptySound;


        public FireWeaponFXData(ParticleSystem weaponFireParticles, Light weaponFireLight, AudioClip reloadSound, AudioClip shootSound, AudioClip emptySound)
        {
            this.weaponFireParticles = weaponFireParticles;
            this.weaponFireLight = weaponFireLight;
            this.reloadSound = reloadSound;
            this.shootSound = shootSound;
            this.emptySound = emptySound;









        }
      



    }

    public FireWeaponData gunData = new FireWeaponData(FIRETYPE.AUTOMATIC, 10f, 0.1f, 200f, 700f, 30, 0.7f);
    public FireWeaponFXData gunFX = new FireWeaponFXData();
    public Camera FPSCAMERA;
    public LayerMask impactMask;
    public GameObject bulletHole;
    public bool isReloading { get; private set; }

    private RaycastHit hit;
    private Ray ray;
    private Recoiler GUNRecoiler;
    private Recoiler CAMRecoiler;
    private AudioSource audioSource;
    private float firingTimer;
    private BulletHitController bulletHitController;

    private void Start()
    {

        // Recolier components references from gun and camera 

        GUNRecoiler = GetComponentInParent<Recoiler>();
        CAMRecoiler = FPSCAMERA.GetComponentInParent<Recoiler>();



        gunData.currentAmmo = gunData.magazineCapacity;

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();


        bulletHitController = GetComponent<BulletHitController>();

    }
    private void Update()
    {
        if(gunFX.weaponFireLight != null)
        
            gunFX.weaponFireLight.enabled=gunFX.weaponFireParticles.isPlaying;
           
        
         
    }
    private void playFX()
    {
        
        if(gunFX.weaponFireParticles !=null){
        //gunFX.weaponFireParticles.transform.parent.Rotate(0f, 0f, Random.Range(0f, 360f));
        gunFX.weaponFireParticles.Play(true); }
    }

    public void shoot(bool fireInput)
    {
        if(Time.time >= firingTimer && fireInput && !isReloading)
        {
            //CAlculo del siguiente tiro
            firingTimer = Time.time + 60 / gunData.fireRate;
            //no bullets in the magazine
            if (gunData.currentAmmo == 0)
            {
                audioSource.PlayOneShot(gunFX.emptySound);
                firingTimer += gunFX.emptySound.length;
                return;

            }

            //Decrease current ammo by one
            gunData.currentAmmo--;
            //apply recoiling
            GUNRecoiler.recoil += gunData.recoil;
            CAMRecoiler.recoil += gunData.recoil;
            //play shoot sound;
            audioSource.PlayOneShot(gunFX.shootSound);
            if (Random.Range(0, 100) < gunData.muzzleFireFrequency * 100)//?
                playFX();

            //  calculate the middle point of the screen
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

            //raycasting from camera
            ray = FPSCAMERA.ScreenPointToRay(screenCenterPoint);

            bulletHitController.handleHit(ray, gunData.range, gunData.power);
          /*  if(Physics.Raycast(ray, out hit, gunData.range, impactMask))
            {
               
                Vector3 bulletHolePosition = hit.point + hit.normal * 0.01f; //?
                //variable to hold the rotation of gun is needed
                Quaternion bulletHoleRotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                GameObject hole = Instantiate(bulletHole, bulletHolePosition,bulletHoleRotation);
                Debug.Log("hola");
            }*/


        }

    }

   private IEnumerator reload()
    {

        isReloading = true;

        audioSource.PlayOneShot(gunFX.reloadSound);
        yield return new WaitForSeconds(gunFX.reloadSound.length);
        gunData.currentAmmo = gunData.magazineCapacity;
        isReloading = false;
    }
  
    public void OnReloadButtonDown()
    {
        if (isReloading == false && gunData.currentAmmo < gunData.magazineCapacity)
        {
            StartCoroutine(reload());

        }
    }
}
    
