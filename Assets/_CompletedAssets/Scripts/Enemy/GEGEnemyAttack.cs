using UnityEngine;
using System.Collections;
using GEGFramework;

namespace CompleteProject {
    public class GEGEnemyAttack : MonoBehaviour, IGEGController {

        [field: SerializeField]
        public GEGCharacter GEGCharacter { get; set; }

        [field: SerializeField]
        public float IntensityScalar { get; set; }

        [field: SerializeField]
        public bool IncreaseIntensity { get; set; }

        public float timeBetweenAttacks;     // The time in seconds between each attack.
        public int attackDamage;               // The amount of health taken away per attack.

        Animator anim;                              // Reference to the animator component.
        GameObject player;                          // Reference to the player GameObject.
        GEGPlayerHealth playerHealth;                  // Reference to the player's health.
        GEGEnemyHealth enemyHealth;                    // Reference to this enemy's health.
        bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
        float timer;                                // Timer for counting up to the next attack.


        void Awake() {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<GEGPlayerHealth>();
            enemyHealth = GetComponent<GEGEnemyHealth>();
            anim = GetComponent<Animator>();
        }

        void OnEnable() {
            string damageName = GEGCharacter.name + "Damage";
            string rateName = GEGCharacter.name + "AttackRate";
            attackDamage = (int)GEGCharacter[damageName].value;
            timeBetweenAttacks = GEGCharacter[rateName].value;
        }


        void OnTriggerEnter(Collider other) {
            // If the entering collider is the player...
            if (other.gameObject == player) {
                // ... the player is in range.
                playerInRange = true;
            }
        }


        void OnTriggerExit(Collider other) {
            // If the exiting collider is the player...
            if (other.gameObject == player) {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }


        void Update() {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) {
                // ... attack.
                Attack();
            }

            // If the player has zero or less health...
            if (playerHealth.currentHealth <= 0) {
                // ... tell the animator the player is dead.
                anim.SetTrigger("PlayerDead");
            }
        }


        void Attack() {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0) {
                // ... damage the player.
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}