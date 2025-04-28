using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static int score;
    int level;
    int rndRole;
    int hScore;
    float span = 10.0f;
    List<Vector3> pPos = new List<Vector3>();
    Vector3 eSetpos;

    [SerializeField]
    GameObject Triangle;
    [SerializeField]
    GameObject Circle;
    [SerializeField]
    GameObject Square;
    [SerializeField]
    GameObject sBar;
    [SerializeField]
    GameObject HSTriangle;
    [SerializeField]
    GameObject TimeBar;
    [SerializeField]
    GameObject ResultBG;

    void rndActive(GameObject player, GameObject enemy, GameObject coin)
    {
        for (int i = 0; i < level / 3 + 1; i++)
        {
            player.tag = "Player";
            player.GetComponent<PlayerController>().enabled = true;
            pPos.Add(new Vector3(Random.Range(-18, 18) / 2.0f, Random.Range(-10, 10) / 2.0f, 0));
            Instantiate(player, pPos[i], Quaternion.identity);
        }

        for (int i = 0; i < level; i++)
        {
            coin.tag = "Coin";
            coin.GetComponent<ScoreController>().enabled = true;
            Instantiate(coin, new Vector3(Random.Range(-17, 17) / 2.0f, Random.Range(-9, 9) / 2.0f, 0), Quaternion.identity);
        }

        for (int i = 0; i < (level + 1) / 2; i++)
        {
            enemy.tag = "Enemy";
            enemy.GetComponent<EnemyController>().enabled = true;

            bool isContinue = false;
            do
            {
                eSetpos = new Vector3(Random.Range(-18, 18) / 2.0f, Random.Range(-10, 10) / 2.0f, 0);
                isContinue = false;
                for (int j = 0; j < pPos.Count; j++)
                {
                    if (Vector3.Distance(eSetpos, pPos[j]) <= 3)
                    {
                        isContinue = true;
                    }
                }
            } while (isContinue);


            Instantiate(enemy, eSetpos, Quaternion.identity);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        hScore = PlayerPrefs.GetInt("hScore", 0);
        HSTriangle.transform.position += new Vector3(0.054f * hScore, 0, 0);
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (span >= 10.0f && level - 1 <= 11)
        {
            span = 0;

             GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
             GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
             GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin"); 

             foreach (GameObject pObj in players)
             {
                 Destroy(pObj);
             }
             foreach (GameObject eObj in enemys)
             {
                 Destroy(eObj);
             }
             foreach (GameObject cObj in coins)
             {
                 Destroy(cObj);
             }

            pPos.Clear();

            Triangle.GetComponent<PlayerController>().enabled = false;
            Square.GetComponent<PlayerController>().enabled = false;
            Circle.GetComponent<PlayerController>().enabled = false;
            Triangle.GetComponent<EnemyController>().enabled = false;
            Square.GetComponent<EnemyController>().enabled = false;
            Circle.GetComponent<EnemyController>().enabled = false;
            Triangle.GetComponent<ScoreController>().enabled = false;
            Square.GetComponent<ScoreController>().enabled = false;
            Circle.GetComponent<ScoreController>().enabled = false;
            Triangle.tag = "Untagged";
            Square.tag = "Untagged";
            Circle.tag = "Untagged";

            level++;

            rndRole = Random.Range(1, 7);
            switch (rndRole) 
            {
                case 1:
                    rndActive(Triangle, Square, Circle);
                    break;
                case 2:
                    rndActive(Triangle, Circle, Square);
                    break;
                case 3:
                    rndActive(Square, Triangle, Circle);
                    break;
                case 4:
                    rndActive(Square, Circle, Triangle);
                    break;
                case 5:
                    rndActive(Circle, Square, Triangle);
                    break;
                case 6:
                    rndActive(Circle, Triangle, Square);
                    break;
            }
        }

        if(level - 1 > 11)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

            foreach (GameObject pObj in players)
            {
                Destroy(pObj);
            }
            foreach (GameObject eObj in enemys)
            {
                Destroy(eObj);
            }
            foreach (GameObject cObj in coins)
            {
                Destroy(cObj);
            }
            Destroy(TimeBar);

            pPos.Clear();

            ResultBG.SetActive(true);

            if(score > hScore)
            {
                hScore = score;
                PlayerPrefs.SetInt("hScore", hScore);
                score = 0;
            }
        }
            span += Time.deltaTime;
    }

}
