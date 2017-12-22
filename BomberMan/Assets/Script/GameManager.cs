using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }

    private const int PIXEL_PER_UNIT = 100;
    private const float TILE_SIZE = 38;

    
    public static bool m_PlayerHasWon = false;

    public GameObject[] m_FloorPrefabList;
    public GameObject[] m_WallPrefabList;

    public GameObject m_FloorPrefab;
    public GameObject m_WallPrefab;
    public GameObject m_BreakableWallPrefab;
    public GameObject m_TrapPrefab;

    public LevelData m_LevelData;

    public GameObject m_Foe1;
    public GameObject m_Foe2;
    public GameObject m_Foe3;

    public Text m_ResultMenuText;

    public Timer m_Timer;
    public PlayerMovement m_PlayerMovement;
    public bool m_GameOver = false;

    public enum ETileType
    {
        Floor,
        Wall,
        BreakableWall,

        Count,
        Random,
    }

    //public int[,] m_Tiles = new int[16, 12] {   { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} };

    //public int[,] m_Tiles = new int[32, 24] {   { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //                                            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} };

    private void Awake()
    {
        m_Instance = this;
  
    }

    public void Start()
    {
        int length = (int)(Screen.width / TILE_SIZE);
        int height = (int)(Screen.height / TILE_SIZE);

        Vector2 initialPos = new Vector2((-Screen.width / 2 + TILE_SIZE / 2) / PIXEL_PER_UNIT, (Screen.height / 2 - TILE_SIZE / 2) / PIXEL_PER_UNIT);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Vector2 spawnPos = initialPos + new Vector2(TILE_SIZE * j / PIXEL_PER_UNIT, -TILE_SIZE * i / PIXEL_PER_UNIT);

                //CreateTile((ETileType)m_Tiles[i, j], spawnPos);

                if (i == 0 || j == 0 || i == height - 1 || j == length - 1 || i == 1 || j == 1 || i == height - 2 || j == length - 2)
                {
                    GameObject wall = Instantiate(m_WallPrefab);
                    wall.transform.position = spawnPos;
                }
                else if (i == 2 || j == 2 || i == height - 3 || j == length - 3)
                {
                    GameObject floor = Instantiate(m_FloorPrefab);
                    floor.transform.position = spawnPos;
                }
                else
                {
                    int rand = Random.Range(0, 100);
                    if (rand < 33)
                    {
                        GameObject wall = Instantiate(m_WallPrefab);
                        wall.transform.position = spawnPos;
                    }
                    else if (rand > 33 && rand < 50)
                    {
                        GameObject breakablewall = Instantiate(m_BreakableWallPrefab);
                        breakablewall.transform.position = spawnPos;
                    }
                    else 
                    {
                        GameObject floor = Instantiate(m_FloorPrefab);
                        floor.transform.position = spawnPos;
                    }
                }

                if(i == 2 && j == 6)
                {
                    GameObject trap = Instantiate(m_TrapPrefab);
                    trap.transform.position = spawnPos;
                }

            }
        }

        
    }

    private void CreateTile(ETileType aType, Vector2 aSpawnPos)
    {
        switch (aType)
        {
            case ETileType.Floor:
                GameObject floor = Instantiate(m_FloorPrefabList[Random.Range(0, m_FloorPrefabList.Length)]);
                floor.transform.position = aSpawnPos;
                break;
            case ETileType.Wall:
                GameObject wall = Instantiate(m_WallPrefabList[Random.Range(0, m_WallPrefabList.Length)]);
                wall.transform.position = aSpawnPos;
                break;
            case ETileType.Random:
                int rand = Random.Range(0, 100);
                if (rand < 33)
                {
                    GameObject wall1 = Instantiate(m_WallPrefab);
                    wall1.transform.position = aSpawnPos;
                }
                else
                {
                    GameObject floor1 = Instantiate(m_FloorPrefab);
                    floor1.transform.position = aSpawnPos;
                }
                break;
        }
    }

    public void Update()
    {
        AllFoeDeadOrTimeIsUp();
    }

    public void DestroyWall(GameObject aWall)
    {
        Instantiate(m_FloorPrefab, aWall.transform.position, Quaternion.identity);
        Destroy(aWall);
    }

    public void AllFoeDeadOrTimeIsUp()
    {
        if((m_Foe1.activeInHierarchy == false && m_Foe2.activeInHierarchy == false && m_Foe3.activeInHierarchy == false) || m_Timer.m_TimeIsUp == true || m_PlayerMovement.m_HealthBar.value == 0)
        {


            if (m_Foe1.activeInHierarchy == false && m_Foe2.activeInHierarchy == false && m_Foe3.activeInHierarchy == false)
            {
                m_PlayerHasWon = true;
            }
            else if (m_Timer.m_TimeIsUp == true || m_PlayerMovement.m_HealthBar.value == 0)
            {
                m_PlayerHasWon = false;
            }

            LoadLevel();
            m_GameOver = true;
        }
        else
        {
            
            m_GameOver = false;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Result");
    }

}


