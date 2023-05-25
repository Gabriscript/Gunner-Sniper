using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct TileData {
    public char symbol;
    public GameObject prefab;


}
public class LevelManager : MonoBehaviour {
    [SerializeField] List<TileData> tileTypes;
    Dictionary<char, GameObject> tilePrefabs;
    string[] levelTest = new string[] {
            "....G....",
            ".........",
            ".........",
            ".........",
            ".........",
            ".........",
            ".........",

        };
    string[] level1 = new string[] {


            ".......G.",
            ".........",
            "....X.X.X",
            ".........",
            ".........",

        };
    string[] level2 = new string[] {

            ".X..X..X.",
            ".........",
            "....G....",
            ".X.......",
            ".......X.",
            ".........",
            "C........",
            ".........",

        };
    string[] level3 = new string[] {

            "....X....",
            ".........",
            "....G....",
            ".X.....X.",
            ".........",
            "....X..X.",
            ".........",
            "....C....",
            ".........",
            ".X.....X.",

        };
    string[][] levels;
    public int levelCount = 0;
    bool loaded = true;
    public float dx = 1f;
    public float dy = 1f;
    public Transform blocksFolderObjects;
    Goal goal;
    GameManager gm;
   
    private void Awake() {

        Time.timeScale = 1f;
        gm = FindObjectOfType<GameManager>();
        levels = new string[][] { levelTest, level1, level2, level3 };
        tilePrefabs = new Dictionary<char, GameObject>();
        foreach (var tileType in tileTypes) {
            tilePrefabs.Add(tileType.symbol, tileType.prefab);
        }
    }

    public void LoadLevel(string[] levelData) {

        gm.shots = 5;
        //Removing the cleared level
        int nChild = transform.childCount;
        for (int i=0; i < nChild; i++) {
            var child = transform.GetChild(i);
            Destroy(child.gameObject);
        }
        // goal.youScored = false;
        int x = 0, y = 0;
        var center = transform.position;

        foreach (string row in levelData) {
            var nY = levelData.Length;
            foreach (char c in row) {
                var nX = row.Length;
                GameObject newBlock =
                      Instantiate(tilePrefabs[c], new Vector2(x, -y), Quaternion.identity);

                newBlock.transform.position =
                    center +
                    x * Vector3.right * dx + Vector3.left * (nX / 2f - 0.5f) * dx +
                    y * Vector3.down * dy + Vector3.up * (nY / 2f - 0.5f) * dy;
                newBlock.transform.parent = blocksFolderObjects;
                // var block = newBlock.GetComponent<ti>();
                // block.manager = this;
                x++;
            }
            y++;
            x = 0;
        }
        goal = FindObjectOfType<Goal>();

    }






    void Start() {

       
      
        LoadLevel(levels[levelCount]);
    }


    void Update() {
        bool inLevel = goal != null;
        if (levelCount <= levels.Length ) {
            if (inLevel) {
                if (goal.goal == true) {
                    levelCount++;
                    LoadLevel(levels[levelCount]);
                }
            }
        } else {

            gm.GameOver();
            levelCount = 0;
        }

     
       

        
    }
}
