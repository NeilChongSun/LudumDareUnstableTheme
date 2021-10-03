using System.Collections;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public enum GameStage
    {
        NULL = 0,
        ONE, // 1 
        TWO, // 2 
        THREE, //3
        FOUR,//4
        FIVE,//5
        SIX,//6
        SEVEN,//7
        LAST//8
    }

    public static GameManger instance = null;

    public GameObject[] enemyPrefabs;
    public GameObject playerPrefab;
    
    [HideInInspector]
    public GameObject player;
    
    public Transform[] enemySpawnPoints;
    
    public GameStage gameStage;

    public StageInformation[] stageInformations;
    private StageInformation currentStageInformation;
    
    public int gameTime;
    private bool isLast = false;

    private Coroutine timeCounterCoroutine = null;
    private Coroutine switchStateCoroutine = null;
    private Coroutine spawnEnemyCoroutine = null;
    private Coroutine breakTimeCountDownCoroutine = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        SpawnPlayer();
        gameStage = GameStage.ONE;
        gameTime = 0;
        timeCounterCoroutine = StartCoroutine(TimeCounter());
    }

    private void Update()
    {
        currentStageInformation = stageInformations[(int)gameStage - 1];

        UpdateState();

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SwitchStageDirectly();
        }
    }

    private void UpdateState()
    {
        if (spawnEnemyCoroutine == null)
        {
            print(gameStage + "start");
            spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(currentStageInformation));
        }

        if (switchStateCoroutine == null)
            switchStateCoroutine = StartCoroutine(StageCountDown(currentStageInformation.stageTime));

        if (gameStage == GameStage.SEVEN && !isLast)
        {
            isLast = true;
        }
    }
    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }

    private void ShowMessage()
    {
        print("Stage: " + gameStage + "Done, this 10 sec break time");
    }

    private IEnumerator SpwanEnemy(StageInformation info)
    {
        for (int i = 0; i < info.enemyCount; i++)
        {
            int index = Random.Range(0, info.enemyPrefabs.Length);

            GameObject enemy = Instantiate(enemyPrefabs[index], enemySpawnPoints[Random.Range(0, 6)].position, Quaternion.identity);
            if (player != null)
            {
                enemy.GetComponent<Enemy>().player = player;
            }
            yield return new WaitForSeconds(info.stageTime / info.enemyCount);
        }
    }
    private IEnumerator BreakTimeCountDown()
    {
        //KeyCodeManager.instance.RandomKeyCodeSet();
        ShowMessage();
        yield return new WaitForSeconds(10);
        if (!isLast)
        {
            gameStage++;
        }
        else
        {
            gameStage = (GameStage)Random.Range(1, (int)GameStage.LAST);
        }
        if (spawnEnemyCoroutine != null)
        {
            StopCoroutine(spawnEnemyCoroutine);
            spawnEnemyCoroutine = null;
        }
        if (switchStateCoroutine != null)
        {
            StopCoroutine(switchStateCoroutine);
            switchStateCoroutine = null;
        }
        breakTimeCountDownCoroutine = null;
        yield break;
    }

    private void SwitchStageDirectly()
    {
        if (switchStateCoroutine != null)
        {
            StopCoroutine(switchStateCoroutine);
        }
        if (spawnEnemyCoroutine != null)
        {
            StopCoroutine(spawnEnemyCoroutine);
        }
        if (breakTimeCountDownCoroutine == null)
        {
            breakTimeCountDownCoroutine = StartCoroutine(BreakTimeCountDown());
        }
    }

    private IEnumerator StageCountDown(int time)
    {
        yield return new WaitForSeconds(time);
        if (breakTimeCountDownCoroutine == null)
        {
            breakTimeCountDownCoroutine = StartCoroutine(BreakTimeCountDown());
        }
        yield break;
    }
    private IEnumerator TimeCounter()
    {
        while (true)
        {
            gameTime++;
            yield return new WaitForSeconds(1);
        }
    }
}
