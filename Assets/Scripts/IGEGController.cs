using System;
using UnityEngine;

namespace GEGFramework {
    public interface IGEGController {
        GEGCharacter GEGCharacter { get; set; }
        float IntensityScalar { get; set; }
        bool IncreaseIntensity { get; set; }
    }
}