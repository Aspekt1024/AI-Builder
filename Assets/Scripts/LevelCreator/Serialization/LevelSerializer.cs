using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelSerializer {
    
    private const string SAVE_PATH = "/Resources/Levels";
    private const string LOAD_PATH = "Levels";
    
    public void Save()
    {
        LevelData data = new LevelData();
        LevelGrid levelGrid = Object.FindObjectOfType<Level>().Grid;
        
        for (int row = 0; row < GridProperties.ROOM_ROWS; row++)
        {
            for (int col = 0; col < GridProperties.ROOM_COLS; col++)
            {
                int index = row * GridProperties.ROOM_COLS + col;
                data.CellData[index] = new CellData
                {
                    Cell = levelGrid.GetCell(row, col)
                };

                foreach (PlaceableObject obj in data.CellData[index].Cell.InhabitingObjects)
                {
                    //data.CellData[index].InhabitingObjects.Add(obj.GetType().ToString());
                    data.CellData[index].InhabitingObjects.Add("\"" + obj.GetType().ToString() + "\":" + JsonUtility.ToJson(obj));
                }
            }
        }

        string json = JsonUtility.ToJson(data, true);
        string path = string.Format("{0}/{1}.json", SAVE_PATH, "test");
        File.WriteAllText(Application.dataPath + path, json);

#if UNITY_EDITOR
        AssetDatabase.ImportAsset("Assets" + path);
#endif

        Debug.Log(path + " saved!");
    }

    public void Load()
    {
        
    }
}

[System.Serializable]
public class LevelData
{
    public CellData[] CellData;

    public LevelData()
    {
        CellData = new CellData[GridProperties.ROOM_COLS * GridProperties.ROOM_ROWS];
    }
}

[System.Serializable]
public class CellData
{
    public Cell Cell;
    public List<string> InhabitingObjects;

    public CellData()
    {
        InhabitingObjects = new List<string>();
    }
}