using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour, ISauvegardable
{
    private static MapGenerator m_Instance;
    public static MapGenerator Instance { get { return m_Instance; } }
    private int length = 16;
    private int height = 16; 
    private const float TILE_SIZE = 1f;
    public GameObject m_Hud;
    public GameObject m_MapGenerator;
    public GameObject m_BlackDoorPrefab;
    public GameObject m_MiddleRockPrefab;
    public GameObject m_BtmLeftCornerRockPrefab;
    public GameObject m_BtmRightCornerRockPrefab;
    public GameObject m_LeftWaterPrefab;
    public GameObject m_MiddleSandPrefab;
    public GameObject m_UpLeftCornerRockPrefab;
    public GameObject m_UpLeftCornerWaterPrefab;
    public GameObject m_UpRightCornerRockPrefab;
    public GameObject m_UpRockPrefab;
    public GameObject m_UpWaterPrefab;
    public GameObject m_MiddleWaterPrefab;
    public GameObject m_LinkPrefab;
    public GameObject m_UnWalkablePrefab;
    public GameObject m_WalkablePrefab;
    public GameObject m_KeyPrefab;
    public GameObject m_RupeePrefab;
    public GameObject m_BombPrefab;
    public GameObject m_OldManPrefab;
    public GameObject m_OldWomanPrefab;
    public GameObject m_ZeldaPrefab;
    public GameObject m_FoePrefab;
    public GameObject m_TriforcePrefab;
    public static GameObject m_FoeCopy;
    public static GameObject m_TriforceCopy;
    public static int WalkableTile = (int)ETilePlayer.Walkable;
    public static int RockTile = (int)ETilePlayer.Rock;
    public static int WaterTile = (int)ETilePlayer.Water;
    public static int KeyTile = (int)ETilePlayer.Key;
    public static int RupeeTile = (int)ETilePlayer.Rupee;
    public static int BombTile = (int)ETilePlayer.Bomb;
    public static int OldManTile = (int)ETilePlayer.OldMan;
    public static int SecretDoor = (int)ETileType.BlackDoor;
    public static Vector2 initialPos = Vector2.zero;

    public enum ETileType
    {
        MiddleWater,         
        MiddleRock,          
        BtmLeftCornerRock,   
        BtmRightCornerRock,  
        LeftWater,           
        MiddleSand,          
        UpLeftCornerRock,    
        UpLeftCornerWater,   
        UpRightCornerRock,   
        UpRock,             
        UpWater,        
        BlackDoor         
    }

    public enum ETilePlayer
    {
        Link,
        Walkable,
        Rock,
        Water,
        Key,
        Rupee,
        Bomb,
        OldMan,
        OldWoman,
        Zelda,
        Foe,
        Triforce
    }

    public static int[,] m_Tiles;

    public static int[,] m_PlayerPos;

    public static IInteractiveObject[,] m_objectsMap;

    private void Awake()
    {
        m_Instance = this;
    }

    public void Start()
    {
        ApplySauvegarde();
        m_objectsMap = new IInteractiveObject[length, height];
        SauvegardeManager.ElementsToRefresh.Add(this);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Vector2 spawnPos = initialPos + new Vector2(TILE_SIZE * i, -TILE_SIZE * j);

                CreateTile((ETileType)m_Tiles[i, j], spawnPos);
                CreatePlayer((ETilePlayer)m_PlayerPos[i, j], spawnPos, i, j);
            }
        }
    }

    public void OnDestroy()
    {
        SauvegardeManager.ElementsToRefresh.Remove(this);
    }

    public void ApplySauvegarde()
    {
        m_Tiles = SauvegardeManager.sauvegarde.m_Tiles;
        m_PlayerPos = SauvegardeManager.sauvegarde.m_PlayerPos;
    }

    public struct Coord
    {
        public int i;
        public int j;
    }

    public static Coord ConvertWorldToIndex(Vector2 position)
    {
        int j = -(int)((position.y - initialPos.y));
        int i = (int)((position.x - initialPos.x - 0.5));

        return new Coord() { i = i, j = j };
    }
    
    private void CreateTile(ETileType aType, Vector2 aSpawnPos)
    {
        switch (aType)
        {
            case ETileType.MiddleWater:
                GameObject middlewater = Instantiate(m_MiddleWaterPrefab);
                middlewater.transform.position = aSpawnPos;
                break;
            case ETileType.MiddleRock:
                GameObject middlerock = Instantiate(m_MiddleRockPrefab);
                middlerock.transform.position = aSpawnPos;
                break;
            case ETileType.BtmLeftCornerRock:
                GameObject btmleftcornerrock = Instantiate(m_BtmLeftCornerRockPrefab);
                btmleftcornerrock.transform.position = aSpawnPos;
                break;
            case ETileType.BtmRightCornerRock:
                GameObject btmrightcornerrock = Instantiate(m_BtmRightCornerRockPrefab);
                btmrightcornerrock.transform.position = aSpawnPos;
                break;
            case ETileType.LeftWater:
                GameObject leftwater = Instantiate(m_LeftWaterPrefab);
                leftwater.transform.position = aSpawnPos;
                break;
            case ETileType.MiddleSand:
                GameObject middlesand = Instantiate(m_MiddleSandPrefab);
                middlesand.transform.position = aSpawnPos;
                break;
            case ETileType.UpLeftCornerRock:
                GameObject upleftcornerrock = Instantiate(m_UpLeftCornerRockPrefab);
                upleftcornerrock.transform.position = aSpawnPos;
                break;
            case ETileType.UpLeftCornerWater:
                GameObject upleftcornerwater = Instantiate(m_UpLeftCornerWaterPrefab);
                upleftcornerwater.transform.position = aSpawnPos;
                break;
            case ETileType.UpRightCornerRock:
                GameObject uprightcornerrock = Instantiate(m_UpRightCornerRockPrefab);
                uprightcornerrock.transform.position = aSpawnPos;
                break;
            case ETileType.UpRock:
                GameObject uprock = Instantiate(m_UpRockPrefab);
                uprock.transform.position = aSpawnPos;
                break;
            case ETileType.UpWater:
                GameObject upwater = Instantiate(m_UpWaterPrefab);
                upwater.transform.position = aSpawnPos;
                break;
            case ETileType.BlackDoor:
                GameObject blackdoor = Instantiate(m_BlackDoorPrefab);
                blackdoor.transform.position = aSpawnPos;
                break;
        }
    }

    public void CreatePlayer(ETilePlayer aType, Vector2 aSpawnPos, int i, int j)
    {
        switch (aType)
        {
            case ETilePlayer.Link:
                GameObject link = Instantiate(m_LinkPrefab);
                link.transform.position = aSpawnPos;
                break;
            case ETilePlayer.Rock:
                GameObject rock = Instantiate(m_UnWalkablePrefab);
                rock.transform.position = aSpawnPos;
                break;
            case ETilePlayer.Water:
                GameObject water = Instantiate(m_WalkablePrefab);
                water.transform.position = aSpawnPos;
                break;
            case ETilePlayer.Key:
                GameObject key = Instantiate(m_KeyPrefab);
                key.transform.position = aSpawnPos;
                break;
            case ETilePlayer.Rupee:
                GameObject rupee = Instantiate(m_RupeePrefab);
                rupee.transform.position = aSpawnPos;
                break;
            case ETilePlayer.Bomb:
                GameObject bomb = Instantiate(m_BombPrefab);
                bomb.transform.position = aSpawnPos;
                break;
            case ETilePlayer.OldMan:
                GameObject oldman = Instantiate(m_OldManPrefab);
                m_objectsMap[i, j] = oldman.GetComponent<OldMan>();
                oldman.transform.position = aSpawnPos;
                break;
            case ETilePlayer.OldWoman:
                GameObject oldwoman = Instantiate(m_OldWomanPrefab);
                m_objectsMap[i, j] = oldwoman.GetComponent<OldWoman>();
                oldwoman.transform.position = aSpawnPos;
                break;
            case ETilePlayer.Zelda:
                GameObject zelda = Instantiate(m_ZeldaPrefab);
                m_objectsMap[i, j] = zelda.GetComponent<Zelda>();
                zelda.transform.position = aSpawnPos;
                break;
            case ETilePlayer.Foe:
                GameObject foe = Instantiate(m_FoePrefab);
                m_FoeCopy = foe;
                m_objectsMap[i, j] = foe.GetComponent<Foe>();
                foe.transform.position = aSpawnPos;
                break;
            case ETilePlayer.Triforce:
                GameObject triforce = Instantiate(m_TriforcePrefab);
                m_TriforceCopy = triforce;
                m_objectsMap[i, j] = triforce.GetComponent<Triforce>();
                triforce.transform.position = aSpawnPos;
                break;
        }
    }
}
