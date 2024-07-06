using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour {
    private static bool initialized = false;
    private static string[] nouns;
    private static string[] adjectives;
    private static int[] backdrops;

    public static string GetName() {
        if (!initialized)
            Initialize();
        string name = "The ";
        name += adjectives[Random.Range(0, adjectives.Length - 1)] + " ";
        name += nouns[Random.Range(0, nouns.Length - 1)];
        return name;
    }

    public static int GetBackdropFromName(string name) {
        string[] parts = name.Split(" ");
        string noun = parts[2];
        for (int i = 0; i < nouns.Length; i++) {
            if (nouns[i].Equals(noun))
                return backdrops[i];
        }
        return -1;
    }

    private static void Initialize() {
        string[] nounLines = Resources.Load<TextAsset>("nouns").ToString().Split("\n");
        adjectives = Resources.Load<TextAsset>("adjectives").ToString().Split("\n");
        nouns = new string[nounLines.Length];
        backdrops = new int[nounLines.Length];

        for (int i = 0; i < nounLines.Length; i++) {
            string[] parts = nounLines[i].Split(";");
            nouns[i] = parts[0];
            backdrops[i] = int.Parse(parts[1]);
            
        }
        
        initialized = true;
    }

}
