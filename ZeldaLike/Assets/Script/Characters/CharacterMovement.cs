using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour, ISauvegardable
{
    public GameObject m_Link;
    private const float TILE_SIZE = 1f;
    private Vector2 m_Xvelocity = new Vector2(1, 0);
    private Vector2 m_Yvelocity = new Vector2(0, 1);
    private float m_Speed;
    public Rigidbody2D m_RigidBody;
    public bool m_IsMoving;
    private Vector2 m_InitialPos;
    private Vector2 m_WantedPos;
    private float m_PercentageCompletion;
    public Animator m_Animator;
    public SpriteRenderer m_SpriteRenderer;
    public Inventory m_Inventory = new Inventory();
    public MapGenerator m_MapGenerator;
    public GameObject m_BlankPrefab;
    public GameObject m_DoorPrefab;
    public GameObject m_BombPrefab;
    public GameObject m_ExplosionPrefab;
    private Bomb m_SpawnedBomb = new Bomb();
    private int m_BombDuration;
    private int m_ExplosionDuration = 2;
    public bool BombIsSpawned;
    public FoeMovement m_FoeMovement;
    public static bool m_AttackMode;
    private bool m_step1;
    private bool m_step2;
    private bool m_step3;
    public static bool m_FoeActivated;
    public static bool m_enemyDefeated;
    public GameObject m_bigTriforce;

    public void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        ApplySauvegarde();
        SauvegardeManager.ElementsToRefresh.Add(this);
        m_Animator.SetBool("isWalking", false);
        m_Animator.SetBool("isWalking", true);
        DontDestroyOnLoad(m_Inventory);
        m_bigTriforce.SetActive(false);
    }

    public void OnDestroy()
    {
        SauvegardeManager.ElementsToRefresh.Remove(this);
    }

    public enum direction
    {
        up,
        down,
        right,
        left
    };

    public direction currentDirection { get; private set; }

    public void ApplySauvegarde()
    {
        this.transform.position = new Vector2(SauvegardeManager.sauvegarde.m_playerX, SauvegardeManager.sauvegarde.m_playerY);
        this.m_Speed = SauvegardeManager.sauvegarde.m_Speed;
        this.m_IsMoving = SauvegardeManager.sauvegarde.m_IsMoving;
        this.m_BombDuration = SauvegardeManager.sauvegarde.m_BombDuration;
    }

    public void FixedUpdate()
    {
        ToMove();
        PickItems();
        PutBomb();
        Attack();
        SaveGame();
        LoadGame();
        SwitchLevel();
        SpeakToPeople();
        InteractionPattern();

        if(m_enemyDefeated)
        {
            MapGenerator.m_FoeCopy.SetActive(false);
            MapGenerator.m_TriforceCopy.SetActive(true);
        }

        if(Triforce.m_triforceTaken)
        {
            MapGenerator.m_TriforceCopy.SetActive(false);
            m_bigTriforce.SetActive(true);
        }
    }

    public void PickItems()
    {
        MapGenerator.Coord myPosition = MapGenerator.ConvertWorldToIndex(transform.position);
        int myActualPosition = GetTile(myPosition.i, myPosition.j);
        Vector2 spawnPos = MapGenerator.initialPos + new Vector2(TILE_SIZE * myPosition.i, -TILE_SIZE * myPosition.j);

        if ((myActualPosition == (int)MapGenerator.KeyTile) && (Input.GetKeyDown(KeyCode.Space)))
        {
            int itemindex = GetTile(myPosition.i, myPosition.j);
            m_Inventory.AddKey(new Key());
            MapGenerator.m_PlayerPos[myPosition.i, myPosition.j] = MapGenerator.WalkableTile;
            GameObject blank = Instantiate(m_BlankPrefab);
            blank.transform.position = spawnPos;
        }
        else if ((myActualPosition == (int)MapGenerator.RupeeTile) && (Input.GetKeyDown(KeyCode.Space)))
        {
            int itemindex = GetTile(myPosition.i, myPosition.j);
            m_Inventory.AddRupee(new Rupee());
            MapGenerator.m_PlayerPos[myPosition.i, myPosition.j] = MapGenerator.WalkableTile;
            GameObject blank = Instantiate(m_BlankPrefab);
            blank.transform.position = spawnPos;
        }
        else if ((myActualPosition == (int)MapGenerator.BombTile) && (Input.GetKeyDown(KeyCode.Space)))
        {
            int itemindex = GetTile(myPosition.i, myPosition.j);
            m_Inventory.AddBomb(m_SpawnedBomb);
            MapGenerator.m_PlayerPos[myPosition.i, myPosition.j] = MapGenerator.WalkableTile;
            GameObject blank = Instantiate(m_BlankPrefab);
            blank.transform.position = spawnPos;
        }
    }

    public void SpeakToPeople()
    {
        IInteractiveObject otherObject = null;
        MapGenerator.Coord myPosition = MapGenerator.ConvertWorldToIndex(transform.position);

        switch(currentDirection)
        {
            case direction.up:
                otherObject = GetInteractiveObject(myPosition.i, myPosition.j - 1);
                break;
            case direction.down:
                otherObject = GetInteractiveObject(myPosition.i, myPosition.j + 1);
                break;
            case direction.left:
                otherObject = GetInteractiveObject(myPosition.i - 1, myPosition.j);
                break;
            case direction.right:
                otherObject = GetInteractiveObject(myPosition.i + 1, myPosition.j);
                break;
        }
        if(otherObject != null && Input.GetKey(KeyCode.Space))
        {
            otherObject.StartInteraction();
        }
    }

    public void ToMove()
    {
        MapGenerator.Coord myPosition = MapGenerator.ConvertWorldToIndex(transform.position);
        float askMoveHorizontal = Input.GetAxisRaw("Horizontal");
        float askMoveVertical = Input.GetAxisRaw("Vertical");
        int indexvalueleft = GetTile(myPosition.i - 1, myPosition.j);
        int indexvalueright = GetTile(myPosition.i + 1, myPosition.j);
        int indexvaluetop = GetTile(myPosition.i, myPosition.j - 1);
        int indexvaluedown = GetTile(myPosition.i, myPosition.j + 1);

        if (!m_IsMoving)
        {
            m_Animator.SetFloat("x", askMoveHorizontal);
            m_Animator.SetFloat("y", askMoveVertical);
            m_Animator.SetBool("isWalking", false);
            m_Animator.SetBool("isIdle", true);

            if (askMoveHorizontal != 0)
            {
                m_PercentageCompletion = 0;
                m_IsMoving = true;
                if (askMoveHorizontal < 0)
                {
                    currentDirection = direction.left;
                    if (indexvalueleft == (int)MapGenerator.WalkableTile || indexvalueleft == ((int)MapGenerator.KeyTile) || indexvalueleft == ((int)MapGenerator.RupeeTile) || indexvalueleft == ((int)MapGenerator.BombTile))
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Xvelocity * TILE_SIZE) * askMoveHorizontal;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("isWalking", true);
                        m_Animator.SetBool("isIdle", false);
                        m_SpriteRenderer.flipX = false;
                    }
                    else
                    {
                        m_IsMoving = false;
                    }
                }
                else if (askMoveHorizontal > 0)
                {
                    currentDirection = direction.right;
                    if (indexvalueright == ((int)MapGenerator.WalkableTile) || indexvalueright == ((int)MapGenerator.KeyTile) || indexvalueright == ((int)MapGenerator.RupeeTile) || indexvalueright == ((int)MapGenerator.BombTile))
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Xvelocity * TILE_SIZE) * askMoveHorizontal;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("isWalking", true);
                        m_Animator.SetBool("isIdle", false);
                        m_SpriteRenderer.flipX = true;
                    }
                    else
                    {
                        m_IsMoving = false;
                    }
                }
                else if (askMoveHorizontal == 0)
                {
                    m_IsMoving = false;
                    m_Animator.SetBool("isWalking", false);
                    m_Animator.SetBool("isIdle", true);
                }
            }
            else if (askMoveVertical != 0)
            {
                m_PercentageCompletion = 0;
                m_IsMoving = true;
                if (askMoveVertical < 0)
                {
                    currentDirection = direction.down;
                    if (indexvaluedown == (int)MapGenerator.WalkableTile || indexvaluedown == ((int)MapGenerator.KeyTile) || indexvaluedown == ((int)MapGenerator.RupeeTile) || indexvaluedown == ((int)MapGenerator.BombTile))
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Yvelocity * TILE_SIZE) * askMoveVertical;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("isWalking", true);
                        m_Animator.SetBool("isIdle", false);
                    }
                    else
                    {
                        m_IsMoving = false;
                    }
                }
                else if (askMoveVertical > 0)
                {
                    currentDirection = direction.up;
                    if (indexvaluetop == (int)MapGenerator.WalkableTile || indexvaluetop == ((int)MapGenerator.KeyTile) || indexvaluetop == ((int)MapGenerator.RupeeTile) || indexvaluetop == ((int)MapGenerator.BombTile))
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Yvelocity * TILE_SIZE) * askMoveVertical;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("isWalking", true);
                        m_Animator.SetBool("isIdle", false);
                    }
                    else
                    {
                        m_IsMoving = false;
                    }
                }
                else if (askMoveVertical == 0)
                {
                    m_IsMoving = false;
                    m_Animator.SetBool("isWalking", false);
                    m_Animator.SetBool("isIdle", true);
                }
            }
        }
        else
        {
            m_PercentageCompletion += Time.fixedDeltaTime * m_Speed;
            m_PercentageCompletion = Mathf.Clamp(m_PercentageCompletion, 0, 1);
            m_RigidBody.transform.position = Vector2.Lerp(m_InitialPos, m_WantedPos, m_PercentageCompletion);
            if (Vector2.Distance(m_RigidBody.transform.position, m_WantedPos) == 0f)
            {
                m_IsMoving = false;
            }
        }
    }

    private int GetTile(int i, int j)
    {
        if (i < 0 || i > MapGenerator.m_PlayerPos.GetLength(0) || j < 0 || j > MapGenerator.m_PlayerPos.GetLength(1))
        {
            return 2;
        }
        else
        {
            return MapGenerator.m_PlayerPos[i, j];
        }
    }

    private IInteractiveObject GetInteractiveObject(int i, int j)
    {
        if (i < 0 || i > MapGenerator.m_objectsMap.GetLength(0) || j < 0 || j > MapGenerator.m_objectsMap.GetLength(1))
        {
            return null;
        }
        else
        {
            return MapGenerator.m_objectsMap[i, j];
        }
    }

    public void PutBomb()
    {
        if (Input.GetKeyDown(KeyCode.B) && m_Inventory.bombCountText.text != "X0")
        {
            StartCoroutine(BombTimer());
        }
    }

    public IEnumerator BombTimer()
    {
        MapGenerator.Coord myPosition = MapGenerator.ConvertWorldToIndex(transform.position);
        Vector2 spawnPos = MapGenerator.initialPos + new Vector2(TILE_SIZE * myPosition.i, -TILE_SIZE * myPosition.j);

        GameObject bomb = Instantiate(m_BombPrefab);
        bomb.transform.position = spawnPos;
        m_Inventory.RemoveBomb(m_SpawnedBomb);
        yield return new WaitForSeconds(m_BombDuration);
        Destroy(bomb);

        GameObject explosion = Instantiate(m_ExplosionPrefab);
        explosion.transform.position = spawnPos;
        yield return new WaitForSeconds(m_ExplosionDuration);
        Destroy(explosion);

        MapGenerator.Coord bombPosition = MapGenerator.ConvertWorldToIndex(spawnPos);
        int bombPos = GetTile(bombPosition.i, bombPosition.j);

        if ((m_FoeMovement.ActualFoePosition.i == bombPosition.i - 2 ) && (m_FoeMovement.ActualFoePosition.j == bombPosition.j - 4))
        {
            MapGenerator.m_FoeCopy.SetActive(false);
            m_enemyDefeated = true;

        }
    }

    public void Attack()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            m_Animator.SetBool("isAttacking", true);
            m_Animator.SetBool("isWalking", false);
            m_Animator.SetBool("isIdle", false);
            m_AttackMode = true;
        }
        else
        {
            m_Animator.SetBool("isAttacking", false);
            m_AttackMode = false;
        }
    }

    public void SaveGame()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(JsonConvert.SerializeObject(m_Inventory));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SauvegardeManager.ExportData("newSave.json");
        }
    }

    public void LoadGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SauvegardeManager.ImportData("newSave.json");
        }
    }

    public void SwitchLevel()
    {
        MapGenerator.Coord myPosition = MapGenerator.ConvertWorldToIndex(transform.position);
        int myActualPosition = GetTile(myPosition.i, myPosition.j);

        if ((myPosition.i == 2 && myPosition.j == 9) || (myPosition.i == 3 && myPosition.j == 9))
        {
            SceneManager.LoadScene("Game2");

            SauvegardeManager.ImportData("GameContentLvl2.json");
        }

        if ((myPosition.i == 2 && myPosition.j == 2) || (myPosition.i == 3 && myPosition.j == 2))
        {
            SceneManager.LoadScene("Game");

            SauvegardeManager.ImportData("GameContentLvl1.json");
        }
    }

    public void InteractionPattern()
    {
        if (!OldMan.m_oldManSpoken && !OldWoman.m_oldWomanSpoken && !Zelda.m_zeldaSpoken)
        {
            m_step1 = true;
        }

        if (OldMan.m_oldManSpoken && !OldWoman.m_oldWomanSpoken && !Zelda.m_zeldaSpoken && m_step1)
        {
            m_step2 = true;
        }

        if (OldMan.m_oldManSpoken && OldWoman.m_oldWomanSpoken && !Zelda.m_zeldaSpoken && m_step2)
        {
            m_step3 = true;
        }

        if (OldMan.m_oldManSpoken && OldWoman.m_oldWomanSpoken && Zelda.m_zeldaSpoken  && m_step3)
        {
            Foe.m_FoeActivated = true;

            MapGenerator.m_FoeCopy.SetActive(true);
        }

        if ((!OldMan.m_oldManSpoken && OldWoman.m_oldWomanSpoken && Zelda.m_zeldaSpoken)||
            (!OldMan.m_oldManSpoken && !OldWoman.m_oldWomanSpoken && Zelda.m_zeldaSpoken)||
            (!OldMan.m_oldManSpoken && OldWoman.m_oldWomanSpoken && !Zelda.m_zeldaSpoken)||
            (OldMan.m_oldManSpoken && !OldWoman.m_oldWomanSpoken && Zelda.m_zeldaSpoken))
        {
            OldMan.m_oldManSpoken = false;
            OldWoman.m_oldWomanSpoken = false;
            Zelda.m_zeldaSpoken = false;
            m_step3 = false;
            m_step2 = false;
            m_step1 = true;
        }
    } 
}
