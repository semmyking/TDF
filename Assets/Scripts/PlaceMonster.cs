using UnityEngine;
using System.Collections;

public class PlaceMonster : MonoBehaviour
{

    public GameObject monsterPrefab;
    private GameObject monster;
    private GameManagerBehavior gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool CanPlaceMonster()
    {
        int cost = monsterPrefab.GetComponent<MonsterData>().levels[0].cost;
        return monster == null && gameManager.Points >= cost;
    }

   
    void OnMouseUp()
    {
        
        if (CanPlaceMonster())
        {
           
            monster = (GameObject) Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Points -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        }
        else if (CanUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().increaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Points -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        }
    }

    private bool CanUpgradeMonster()
    {
        if (monster != null)
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>();
            MonsterLevel nextLevel = monsterData.getNextLevel();
            if (nextLevel != null)
            {
                return gameManager.Points >= nextLevel.cost;
            }
        }
        return false;
    }

}
