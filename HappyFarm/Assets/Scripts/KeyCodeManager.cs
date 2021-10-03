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
    }

    public void RandomKeyCodeSet()
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
