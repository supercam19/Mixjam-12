using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGoalReference : MonoBehaviour
{
    void Awake() {
        GameInfo.goalText = GetComponent<UnityEngine.UI.Text>();
    }

    void OnDestroy() {
        GameInfo.goalText = null;
    }
}
