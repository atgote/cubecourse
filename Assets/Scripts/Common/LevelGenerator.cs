// 2021.03.20 Tihonovschi Andrei
// LevelGenerator component
// allows loading and dynamically building levels

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    // prefabs
    GameObject ice = null;
    GameObject fire = null;
    GameObject cube = null;
    GameObject block = null;
    GameObject crystal = null;
    GameObject finish = null;

    // Start is called before the first frame update
    void Start()
    {
        if (! LoadResources())
        {
            return;
        }

        string[] level_data = LoadLevel(Game.GetLevel()); // try to load current game level data
        if (level_data == null) // if failed - return to menu
        {
            SceneManager.LoadScene("Menu");
            return;
        }

        // build level scene
        GenerateLevel(level_data);
    }

    // Load prefabs (object templates)
    bool LoadResources()
    {
        ice = Resources.Load("Prefabs/Ice") as GameObject;
        if (ice == null)
        {
            Debug.Log("Cannot load Ice prefab");
            return false;
        }
        fire = Resources.Load("Prefabs/Fire") as GameObject;
        if (fire == null)
        {
            Debug.Log("Cannot load Fire prefab");
            return false;
        }
        cube = Resources.Load("Prefabs/Cube") as GameObject;
        if (cube == null)
        {
            Debug.Log("Cannot load Cube prefab");
            return false;
        }
        block = Resources.Load("Prefabs/Block") as GameObject;
        if (block == null)
        {
            Debug.Log("Cannot load Block prefab");
            return false;
        }
        crystal = Resources.Load("Prefabs/Crystal") as GameObject;
        if (crystal == null)
        {
            Debug.Log("Cannot load Crystal prefab");
            return false;
        }
        finish = Resources.Load("Prefabs/Finish") as GameObject;
        if (finish == null)
        {
            Debug.Log("Cannot load Finish prefab");
            return false;
        }

        return true;
    }

    // Load level data
    string[] LoadLevel(int level)
    {
        // Level files should be named as "level_xx.txt" and placed in "Levels" folder
        string resource_name = "Levels/level_" + level.ToString("D2");
        TextAsset data = Resources.Load(resource_name) as TextAsset;
        if (data == null) // failed?
        {
            return null;
        }

        // parse to string array
        string[] level_data = data.text.Split(new[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None);

        return level_data;
    }

    // create objects by type (character identifying object type)
    GameObject CreateObject(int type, Vector3 pos) 
    {
        GameObject inst = null;
        switch (type)
        {
            case '.':
                inst = Instantiate(ice, pos, Quaternion.identity);
                break;
            case '#':
                inst = Instantiate(fire, pos, Quaternion.identity);
                break;
            case '0':
                pos.y -= 0.01f; // a little hack to avoid collisions...
                inst = Instantiate(block, pos, Quaternion.identity);
                break;
            case 'a':
                inst = Instantiate(cube, pos, Quaternion.identity);
                break;
            case '*':
                inst = Instantiate(crystal, pos, Quaternion.identity);
                break;
            case '=':
                inst = Instantiate(finish, pos, Quaternion.identity);
                break;
            default:
                break;
        }
        return inst;
    }

    // This method builds scene parsing string level data
    // the level file consists of rows of 5 characters each
    // Every character in a row means:
    // .        - will create an empty "Ice" block - clear road
    // #        - will create a fire / lava block
    // *        - will create an Ice block with a crystal on top of it 
    // 0-9 (N)  - will create a block-tower with N blocks in height (10 if N=0)
    // a-j (N)  - will create a cube-tower with N cubes in height (1 for a, 10 for j)
    void GenerateLevel(string[] level_data)
    {
        Vector3 pos = new Vector3(0, 0, 0);

        for (int row = 0; row < level_data.Length; ++row)
        {
            pos.z = -2.0f;
            if (level_data[row].Length == 5)
            {
                for (int col = 0; col < 5; ++col)
                {
                    int num = 1;
                    int type = level_data[row][col];
                    if (type >= '0' && type <= '9')
                    {
                        num = type - '0';
                        type = '0';
                        if (num == 0)
                        {
                            num = 10;
                        }
                        CreateObject('.', pos);
                    }
                    else if (type >= 'a' && type <= 'j')
                    {
                        num = 1 + type - 'a';
                        type = 'a';
                        CreateObject('.', pos);
                    }
                    else if (type == '*')
                    {
                        CreateObject('.', pos);
                    }
                    
                    GameObject inst = null;
                    for (int i = 0; i < num; ++i)
                    {
                        GameObject ninst = CreateObject(type, pos);
                        // cubes will be stacked
                        if (type == 'a')
                        {
                            ninst.GetComponent<Stacked>().Stack(inst);
                        }
                        inst = ninst;
                        pos.y += 1.0f;
                    }

                    pos.y = 0;
                    pos.z += 1;
                }

                pos.x += 1;
            }
        }
    }
}