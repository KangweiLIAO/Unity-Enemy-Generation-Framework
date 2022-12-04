using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GEGFramework
{

    public class GEGWaveManager : MonoBehaviour
    {
        [SerializeField]
        GEGWave easyModeWave;

        [SerializeField]
        GEGWave normalModeWave;

        [SerializeField]
        GEGWave hardModeWave;

        [SerializeField]
        IntensityManager IntensityManage;
        // Start is called before the first frame update
        void Awake()
        {
            if (easyModeWave != null)
            {
                IntensityManage.easyModeDuration = easyModeWave.numKeepTurn;
            }
            if (hardModeWave != null)
            {
                IntensityManage.hardModeDuration = hardModeWave.numKeepTurn;
            }
            IntensityManager.OnModeChanged += (GameMode mode) => {
                apply(mode);
            };
        }

        // Update is called once per frame
        void Update()
        {

        }
        void apply(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Easy:
                    InEasyMode();
                    break;
                case GameMode.Normal:
                    InNormalMode();
                    break;
                case GameMode.Hard:
                    InHardMode();
                    break;
            }
        }

        void InEasyMode()
        {
            if (easyModeWave != null)
            {
                Debug.Log("InEasyMode");
                foreach (GEGCharacter c in PackedData.Instance.characters)
                {
                    c.numNextWave = 0;
                }
                foreach (GEGUpdateCharacter c in easyModeWave.characters)
                {
                    foreach (GEGCharacter c2 in PackedData.Instance.characters)
                    {
                        if (c.characters == c2)
                        {
                            Debug.Log("equal");
                            c2.numNextWave = c.numberNextTurn;
                            for (int i = 0; i < c.properties.Count; i++)
                            {
                                foreach (GEGProperty prop in c2.properties)
                                {
                                    if (prop.enabled && (prop == c.properties[i].getproperty()))
                                    {
                                        Debug.Log(c.properties[i].getproperty() + "from " + prop.value + " update to " + c.properties[i].getvalue());
                                        // if the property enabled for evaluation
                                        prop.value = c.properties[i].getvalue();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void InNormalMode()
        {
            if (normalModeWave != null)
            {
                Debug.Log("InNormalMode");
                foreach (GEGCharacter c in PackedData.Instance.characters)
                {
                    c.numNextWave = 0;
                }
                foreach (GEGUpdateCharacter c in normalModeWave.characters)
                {
                    foreach (GEGCharacter c2 in PackedData.Instance.characters)
                    {
                        if (c.characters == c2)
                        {
                            Debug.Log("equal");
                            c2.numNextWave = c.numberNextTurn;
                            for (int i = 0; i < c.properties.Count; i++)
                            {
                                foreach (GEGProperty prop in c2.properties)
                                {
                                    if (prop.enabled && (prop == c.properties[i].getproperty()))
                                    {
                                        Debug.Log(c.properties[i].getproperty() + "from " + prop.value + " update to " + c.properties[i].getvalue());
                                        // if the property enabled for evaluation
                                        prop.value = c.properties[i].getvalue();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void InHardMode()
        {
            if (hardModeWave != null)
            {
                Debug.Log("InHardMode");
                foreach (GEGCharacter c in PackedData.Instance.characters)
                {
                    c.numNextWave = 0;
                }
                foreach (GEGUpdateCharacter c in hardModeWave.characters)
                {
                    foreach (GEGCharacter c2 in PackedData.Instance.characters)
                    {
                        if (c.characters == c2)
                        {
                            Debug.Log("equal");
                            c2.numNextWave = c.numberNextTurn;
                            for (int i = 0; i < c.properties.Count; i++)
                            {
                                foreach (GEGProperty prop in c2.properties)
                                {
                                    if (prop.enabled && (prop == c.properties[i].getproperty()))
                                    {
                                        Debug.Log(c.properties[i].getproperty() + "from " + prop.value + " update to " + c.properties[i].getvalue());
                                        // if the property enabled for evaluation
                                        prop.value = c.properties[i].getvalue();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}