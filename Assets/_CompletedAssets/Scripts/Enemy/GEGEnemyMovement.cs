using UnityEngine;
using System.Collections;
using GEGFramework;

namespace CompleteProject {
    public class GEGEnemyMovement : MonoBehaviour, IGEGController {

        [field: SerializeField]
        public GEGCharacter GEGCharacter { get; set; }

        [field: SerializeField]
        public float IntensityScalar { get; set; }

        [field: SerializeField]
        public bool IncreaseIntensity { get; set; }

        Transform player;                  // Reference to the player's position.
        GEGPlayerHealth playerHealth;      // Reference to the player's health.
        GEGEnemyHealth enemyHealth;        // Reference to this enemy's health.
        UnityEngine.AI.NavMeshAgent nav;   // Reference to the nav mesh agent.


        void Awake() {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth = player.GetComponent<GEGPlayerHealth>();
            enemyHealth = GetComponent<GEGEnemyHealth>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        void OnEnable() {
            string name = GEGCharacter.name + "Speed";
            nav.speed = GEGCharacter[name].value;
            nav.acceleration = Mathf.Clamp(nav.speed / 10, 8, 15);
        }

        void Update() {
            // If the enemy and the player have health left...
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
                // ... set the destination of the nav mesh agent to the player.
                nav.SetDestination(player.position);
            }
            // Otherwise...
            else {
                // ... disable the nav mesh agent.
                nav.enabled = false;
            }
        }
    }
}