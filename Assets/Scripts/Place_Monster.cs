using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Monster : MonoBehaviour
{
    public GameObject monsterPrefab;
    private GameObject monster;
    private GameManagerBehavior gameManager;

    // Start is called before the first frame update
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
        //return monster == null;
        int cost = monsterPrefab.GetComponent<Monster_Data>().levels[0].cost;
        return monster == null && gameManager.Gold >= cost;
    }

    void OnMouseUp()
    {
        if (CanPlaceMonster())
        {
            monster = (GameObject)
                Instantiate(monsterPrefab, transform.position, Quaternion.identity);

            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= monster.GetComponent<Monster_Data>().CurrentLevel.cost;
        }
        else if (CanUpgradeMonster())
        {
            Monster_Data monsterData = monster.GetComponent<Monster_Data>();
            MonsterLevel nextLevel = monsterData.GetNextLevel();
            if (nextLevel != null)
            {
                monsterData.IncreaseLevel();
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(audioSource.clip);
                gameManager.Gold -= nextLevel.cost;
            }
        }
    }

    private bool CanUpgradeMonster()
    {
        if (monster != null)
        {
            Monster_Data monsterData = monster.GetComponent<Monster_Data>();
            MonsterLevel nextLevel = monsterData.GetNextLevel();
            if (nextLevel != null)
            {
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }
}
