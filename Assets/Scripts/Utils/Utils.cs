using System;

namespace GEGFramework {
    public enum CharacterType {
        Player,
        Enemy
    }

    public enum GameMode {
        Easy,
        Normal,
        Hard
    }

    [Serializable]
    public class GEGCharacterException : Exception {
        public GEGCharacterException(string message) : base(message) { }
    }
}