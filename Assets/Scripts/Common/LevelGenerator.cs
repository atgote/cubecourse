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

        string[] level_data = LoadLevel(Game.GetLevel());
        if (level_data == null)
        {
            SceneManager.LoadScene("Menu");
            return;
        }

        GenerateLevel(level_data);
    }

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

    string[] LoadLevel(int level)
    {
        string resource_name = "Levels/level_" + level.ToString("D2");

        TextAsset data = Resources.Load(resource_name) as TextAsset;
        if (data == null)
        {
            return null;
        }

        string[] level_data = data.text.Split(new[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None);

        return level_data;
    }

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
                    else if (type == '*' || type == '&')
                    {
                        CreateObject('.', pos);
                    }
                    
                    GameObject inst = null;
                    for (int i = 0; i < num; ++i)
                    {
                        GameObject ninst = CreateObject(type, pos);
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