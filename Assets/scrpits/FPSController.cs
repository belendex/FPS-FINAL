
using UnityEngine;
[System.Serializable]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(MouseLook))]
public class FPSController : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private MouseLook mouseLook;
    private GunAiming gunAiming;
    private FireWeapon fireWeapon;

  // public GameObject camerasParent;
   // public GameObject FPSCAMERA;
  //  public float walkSpeed = 4f;
   // public float hRotationSpeed = 100f;
   // public float vRotationSpeed = 80f;
  //  public float sprintSpeed = 8f;

   private void Start()
    {
        

        //Esconde y bloquea el ratón
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterMovement = GetComponent<CharacterMovement>();
        mouseLook = GetComponent<MouseLook>();
        gunAiming = GetComponentInChildren<GunAiming>(); //?
        fireWeapon = GetComponentInChildren<FireWeapon>();
    }

    // Update is called once per frame
   private void Update()
    {
         movement();
        rotation();
        aiming();
        shooting();
    }
    private void aiming()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            gunAiming.OnButtonDown();

        }
        else if (Input.GetButtonUp("Fire2"))
        {
            gunAiming.OnButtonUp();
        }
    }
    




        private void movement() {
        //movimiento personaje  
        float hMovementInput = Input.GetAxisRaw("Horizontal");
        float vMovementInput = Input.GetAxisRaw("Vertical");

        bool jumpInput = Input.GetButtonDown("Jump");
        bool dashInput = Input.GetButton("Dash");
        //Rotacion
        //float vCamRotation = Input.GetAxis("Mouse Y") * vRotationSpeed * Time.deltaTime;
        // float hPlayerRotation = Input.GetAxis("Mouse X") * hRotationSpeed * Time.deltaTime;

        //  transform.Rotate(0f, hPlayerRotation, 0f);
        // camerasParent.transform.Rotate(-vCamRotation, 0f, 0f);
        characterMovement.moveCharacter(hMovementInput, vMovementInput, jumpInput, dashInput);
    }
    private void rotation()
    {
        float hRotationInput = Input.GetAxis("Mouse X");
        float vRotationInput = Input.GetAxis("Mouse Y");
        mouseLook.handleRotation(hRotationInput, vRotationInput);
    }
   // private void sprint()
   // {
          //  if (Input.GetKey(KeyCode.LeftShift))
         //   {
          //      walkSpeed = sprintSpeed;
         //   }
           // else
           // {
           //     walkSpeed = 4f;
           // }
  private void shooting()
  {
        if (Input.GetKeyDown(KeyCode.R))
        {
            fireWeapon.OnReloadButtonDown();
        }
        else
        {

            switch (fireWeapon.gunData.firetype)
            {
                case FIRETYPE.REPEATER:
                case FIRETYPE.SEMIAUTOMATIC:
                    fireWeapon.shoot(Input.GetButtonDown("Fire1"));
                    break;
                case FIRETYPE.AUTOMATIC:
                    fireWeapon.shoot(Input.GetButton("Fire1"));
                    break;


            }
        }
            

            
  }
}


    

