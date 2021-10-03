using System.Collections;
using UnityEngine;
public class KeyCodeManager : MonoBehaviour
{
    public static KeyCodeManager instance = null;


    public KeyCodeSet currentKeyCodeSet;
    public KeyCodeSet[] normalKeyCodeSets;
    public KeyCodeSet[] hardCodeSets;


    [Range(0, 100)]
    public int hardPercentage = 0;
    private int normalPercentage = 100;

    public int switchKeyCodeSetInternal = 10;

    private int timer;

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
        normalPercentage = 100 - hardPercentage;
        timer = switchKeyCodeSetInternal;
        StartCoroutine(SwitchKeyCodeTimer());
    }

    private void Update()
    {
        if (timer <= 0)
        {
            RandomKeyCodeSet();
            timer = switchKeyCodeSetInternal;
        }
    }

    private IEnumerator SwitchKeyCodeTimer()
    {
        while (true)
        {
            --timer;
            yield return new WaitForSeconds(1);
        }
    }

    private void RandomKeyCodeSet()
    {
        if (Random.Range(0, 100) >= hardPercentage)
        {
            currentKeyCodeSet = normalKeyCodeSets[Random.Range(0, normalKeyCodeSets.Length)];
        }
        else
        {
            currentKeyCodeSet = hardCodeSets[Random.Range(0, hardCodeSets.Length)];
        }
    }
}
