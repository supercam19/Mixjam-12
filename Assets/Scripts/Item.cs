
using UnityEngine;

public class Item {
    public int id;
    public int count;
    public string name;
    public float price;
    
    public Item(int id, string name, int price = 0) {
        this.id = id;
        this.name = name;
        this.price = price;
    }

    public static Sprite GetSprite(int id) {
        return Resources.Load<Sprite>("sprite_by_id/" + id);
    }

    public string ToString() {
        return name + "x" + count;
    }
}
