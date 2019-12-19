using UnityEngine;

public class CharacterMeleDamage : MonoBehaviour {

    public int damage;    //Damage the player will receive when enter the trigger
    public string targetDamageTag = "FPSController";

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag(targetDamageTag)) {

            CharacterHealth cHealth = other.GetComponent<CharacterHealth>();

            if(cHealth)
            cHealth.hurt(damage);
        }
    }
}

