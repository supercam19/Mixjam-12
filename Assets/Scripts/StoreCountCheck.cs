using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCountCheck : MonoBehaviour {
    private Text text;
    private Item tracking;
    
    public int id;
    public string name;
    public int purchaseCount;
    
    
    void Awake() {
        text = GetComponent<Text>();
        
    }

    void Update() {
        tracking = GameInfo.inventory.GetItem(id);
        text.text = name + " (x" + tracking.count + "/x" + purchaseCount + ")";
    }
}
