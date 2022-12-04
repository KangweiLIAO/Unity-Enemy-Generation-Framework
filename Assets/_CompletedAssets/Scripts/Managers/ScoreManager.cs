using UnityEngine;
using TMPro;

namespace CompleteProject {
    public class ScoreManager : MonoBehaviour {
        public static int score;        // The player's score.
        TextMeshProUGUI scoreText;           // Reference to the Text component.
        [SerializeField] TextMeshProUGUI summonText;

        void Awake() {
            // Set up the reference.
            scoreText = GetComponent<TextMeshProUGUI>();

            // Reset the score.
            score = 0;
        }

        void Update() {
            // Set the displayed text to be the word "Score" followed by the score value.
            scoreText.text = "Score: " + score;
            summonText.text = "Summon Cost: " + GameObject.Find("GEG Player").
                GetComponent<GEGPlayerHealth>().summonCost;
        }
    }
}