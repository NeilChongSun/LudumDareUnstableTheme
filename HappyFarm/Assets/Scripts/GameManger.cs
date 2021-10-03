using System.Collections;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public enum GameStage
    {
        BREAK = 0,
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

    public int gameTime;
    private int timer;
    private bool isLast = false;

    private Coroutine timeCounterCoroutine = null;
    private Coroutine switchStateCoroutine = null;
    private Coroutine spawnEnemyCoroutine = null;

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
        UpdateState();
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SwitchStageDirectly();
        }
    }

    private void UpdateState()
    {
        //switch (gameState)
        //{
        //    case GameState.BREAK:
        //        break;
        //    case GameState.ONE:
        //        if (spawnEnemyCoroutine == null)
        //            spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(1, 5, 1));
        //        if (switchStateCoroutine == null)
        //            switchStateCoroutine = StartCoroutine(SwitchState(5));
        //        break;
        //    case GameState.TWO:
        //        if (spawnEnemyCoroutine == null)
        //            spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(15, 30, 2));
        //        if (switchStateCoroutine == null)
        //            switchStateCoroutine = StartCoroutine(SwitchState(30));
        //        break;
        //    case GameState.THREE:
        //        if (spawnEnemyCoroutine == null)
        //            spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(20, 40, 3));
        //        if (switchStateCoroutine == null)
        //            switchStateCoroutine = StartCoroutine(SwitchState(40));
        //        break;
        //    case GameState.FOUR:
        //        if (spawnEnemyCoroutine == null)
        //            spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(28, 45, 3));
        //        if (switchStateCoroutine == null)
        //            switchStateCoroutine = StartCoroutine(SwitchState(45));
        //        break;
        //    case GameState.FIVE:
        //        if (spawnEnemyCoroutine == null)
        //            spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(30, 50, 4));
        //        if (switchStateCoroutine == null)
        //            switchStateCoroutine = StartCoroutine(SwitchState(50));
        //        break;
        //    case GameState.SIX:
        //        if (spawnEnemyCoroutine == null)
        //            spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(35, 55, 4));
        //        if (switchStateCoroutine == null)
        //            switchStateCoroutine = StartCoroutine(SwitchState(55));
        //        break;
        //    case GameState.SEVEN:
        //        if (spawnEnemyCoroutine == null)
        //            spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(40, 65, 5));
        //        if (switchStateCoroutine == null)
        //            switchStateCoroutine = StartCoroutine(SwitchState(65));
        //        break;
        //    case GameState.LAST:
        //        isLast = true;
        //        break;
        //    default:
        //        break;
        //}
        switch (gameStage)
        {
            case GameStage.BREAK:
                break;
            case GameStage.ONE:
                if (spawnEnemyCoroutine == null)
                    spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(1, 5, 1));
                if (switchStateCoroutine == null)
                    switchStateCoroutine = StartCoroutine(SwitchStage(5));
                break;
            case GameStage.TWO:
                if (spawnEnemyCoroutine == null)
                    spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(1, 3, 2));
                if (switchStateCoroutine == null)
                    switchStateCoroutine = StartCoroutine(SwitchStage(3));
                break;
            case GameStage.THREE:
                if (spawnEnemyCoroutine == null)
                    spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(2, 4, 3));
                if (switchStateCoroutine == null)
                    switchStateCoroutine = StartCoroutine(SwitchStage(4));
                break;
            case GameStage.FOUR:
                if (spawnEnemyCoroutine == null)
                    spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(2, 4, 3));
                if (switchStateCoroutine == null)
                    switchStateCoroutine = StartCoroutine(SwitchStage(4));
                break;
            case GameStage.FIVE:
                if (spawnEnemyCoroutine == null)
                    spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(3, 5, 4));
                if (switchStateCoroutine == null)
                    switchStateCoroutine = StartCoroutine(SwitchStage(5));
                break;
            case GameStage.SIX:
                if (spawnEnemyCoroutine == null)
                    spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(3, 5, 4));
                if (switchStateCoroutine == null)
                    switchStateCoroutine = StartCoroutine(SwitchStage(5));
                break;
            case GameStage.SEVEN:
                if (!isLast)
                    isLast = true;
                if (spawnEnemyCoroutine == null)
                    spawnEnemyCoroutine = StartCoroutine(SpwanEnemy(4, 6, 5));
                if (switchStateCoroutine == null)
                    switchStateCoroutine = StartCoroutine(SwitchStage(6));
                break;
            case GameStage.LAST:
                isLast = true;
                break;
            default:
                break;
        }
    }
    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }

    private void ShowMessage()
    {

    }

    private IEnumerator SpwanEnemy(float count, float time, int max)
    {
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, max);
            if (index >= enemyPrefabs.Length)
            {
                index = enemyPrefabs.Length - 1;
            }
            GameObject enemy = Instantiate(enemyPrefabs[index], enemySpawnPoints[Random.Range(0, 6)].position, Quaternion.identity);
            if (player != null)
            {
                enemy.GetComponent<Enemy>().player = player;
            }
            yield return new WaitForSeconds(time / count);
        }
    }

    private IEnumerator BreakTime()
    {
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
        yield break;
    }

    private void SwitchStageDirectly()
    {
        if (switchStateCoroutine != null)
        {
            StopCoroutine(switchStateCoroutine);
            switchStateCoroutine = null;
        }

        if (spawnEnemyCoroutine != null)
        {
            StopCoroutine(spawnEnemyCoroutine);
            spawnEnemyCoroutine = null;
        }
        StartCoroutine(BreakTime());
        if (switchStateCoroutine != null)
        {
            StopCoroutine(switchStateCoroutine);
            switchStateCoroutine = null;
        }
    }

    private IEnumerator SwitchStage(int time)
    {
        yield return new WaitForSeconds(time);
        if (spawnEnemyCoroutine != null)
        {
            StopCoroutine(spawnEnemyCoroutine);
            spawnEnemyCoroutine = null;
        }
        StartCoroutine(BreakTime());
        switchStateCoroutine = null;
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
