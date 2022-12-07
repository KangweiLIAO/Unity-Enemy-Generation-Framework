using CompleteProject;
using UnityEngine;
using TMPro;

public class DebugUIController : MonoBehaviour {
    // To make the UI stay the same angle
    Quaternion fixedAngle;

    // UI components that need to be updated
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI rateText;
    [SerializeField] TextMeshProUGUI speedText;

    // The health Script. Because it is used in update() so we store it as a class variable.
    GEGEnemyHealth healthScript;

    // Start is called before the first frame update
    void Start() {
        //record the initial angle 
        fixedAngle = transform.rotation;

        // Find health Script
        healthScript = transform.parent.GetComponent<GEGEnemyHealth>();

        // Update UI
        if (hpText && damageText && rateText && speedText) {
            hpText.text = "HP: " + healthScript.currentHealth.ToString();
            damageText.text = "Damage: " + Mathf.Round(transform.parent.GetComponent<GEGEnemyAttack>().attackDamage).ToString();
            rateText.text = "AttackRate: " + transform.parent.GetComponent<GEGEnemyAttack>().timeBetweenAttacks.ToString();
            speedText.text = "Speed: " + transform.parent.GetComponent<UnityEngine.AI.NavMeshAgent>().speed.ToString();
        }
    }
    void Update() {
        // Only health changes during gameplay
        if (hpText && healthScript)
            hpText.text = "HP: " + healthScript.currentHealth.ToString();
    }

    // remain the angle
    void LateUpdate() {
        transform.rotation = fixedAngle;
    }
}
