using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFrameRate : MonoBehaviour
{
    void Start()
    {
        // Some people's video cards turn into a jet engine when Unity runs.
        // This apparently fixes it.
        Application.targetFrameRate = 144; // the maximum reasonable value
    }
}