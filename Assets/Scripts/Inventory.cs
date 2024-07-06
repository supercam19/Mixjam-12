using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [NonSerialized]
    public Item[] items;
    void Start() {
        items = LoadItems();
        SetCount(0, 50);
        DontDestroyOnLoad(this);
    }

    public Item GetItem(int id) {
        return items[id];
    }

    public void ChangeCount(int id, int change) {
        items[id].count += change;
    }

    public void SetCount(int id, int count) {
        items[id].count = count;
    }

    private Item[] LoadItems() {
        string itemData = Resources.Load<TextAsset>("items").ToString();
        string[] items = itemData.Split("\n");
        int i = 0;
        Item[] itemArray = new Item[items.Length];
        foreach (string item in items) {
            string[] info = item.Split(";");
            itemArray[i] = new Item(i, info[0]);
            i++;
        }
        return itemArray;
    }
}
