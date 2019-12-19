using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunAiming : MonoBehaviour {
    [System.Serializable]
    public struct GunAimingData {
        public Transform gunTransform;
        public float aimingZoom;
        public float hipZoom;
        public Vector3 aimingPos;
        public Vector3 hipPos;
        public float aimingSpeed;

        public GunAimingData(Transform gunTransform, float aimingZoom, float hipZoom, Vector3 aimingPos, Vector3 hipPos, float aimingSpeed)
        {
            this.gunTransform = gunTransform;
            this.aimingZoom = aimingZoom;
            this.hipZoom = hipZoom;
            this.aimingPos = aimingPos;
            this.hipPos = hipPos;
            this.aimingSpeed = aimingSpeed;









        }




    }

    public Camera FPSCamera;
    public bool lockAiming = false;
    public GunAimingData gunAimingConfig = new GunAimingData(null, 30f, 60f, Vector3.zero, Vector3.zero, 10f);
    private Vector3 smoother;
    private float Fsmoother;
    public bool isAiming { get; private set; } //?


    void Update()
    {
        float aimSpeed = gunAimingConfig.aimingSpeed * Time.deltaTime;
        if (isAiming == true)
        {
            gunAimingConfig.gunTransform.localPosition = Vector3.SmoothDamp(gunAimingConfig.gunTransform.localPosition, gunAimingConfig.aimingPos, ref smoother, aimSpeed);
            FPSCamera.fieldOfView = Mathf.SmoothDamp(FPSCamera.fieldOfView, gunAimingConfig.aimingZoom, ref Fsmoother, aimSpeed);
        }

        else
        {
            gunAimingConfig.gunTransform.localPosition = Vector3.SmoothDamp(gunAimingConfig.gunTransform.localPosition, gunAimingConfig.hipPos, ref smoother, aimSpeed);
            FPSCamera.fieldOfView = Mathf.SmoothDamp(FPSCamera.fieldOfView, gunAimingConfig.hipZoom, ref Fsmoother, aimSpeed);


        }
    }
        public void OnButtonDown()
        {
            isAiming = (lockAiming) ? !isAiming : true;
        }
        public void OnButtonUp()
        {
        if (!lockAiming) isAiming = false;


        }

    
}
