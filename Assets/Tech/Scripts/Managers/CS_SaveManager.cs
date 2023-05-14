using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SaveManager : MonoBehaviour
{
    public SaveEntry[] _saveEntries;
    public SaveEntry[] SaveEntries => _saveEntries;

    private void Start()
    {
        _saveEntries = new SaveEntry[2] {
        new SaveEntry(10, "oui", 4), new SaveEntry(5, "non", 5)
        };

        Debug.Log(JsonUtility.ToJson(_saveEntries));
    }
}

[System.Serializable]
public class SaveEntry
{
    public int Rank;
    public string Name;
    public int Number;

    public SaveEntry(int rank, string name, int number)
    {
        Rank = rank;
        Name = name;
        Number = number;
    }
}