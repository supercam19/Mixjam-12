using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static bool RandomChance(int percent) {
        return Random.Range(0, 100) < percent;
    }
}
