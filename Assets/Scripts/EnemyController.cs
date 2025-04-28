using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    private float cnt;
    private float span = 10.0f;
    [SerializeField]
    float moveflag;
    GameObject[] player;
    Vector3 pPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        span += Time.deltaTime;
        cnt += Time.deltaTime;

        if (span >= 10.0f)
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            span = 0;
        }
        if (cnt >= moveflag)
        {
            int mvRnd = Random.Range(0, 3);
            Vector3 pos = this.transform.position;

            for(int i = 0; i < player.Length; i++) 
            {
                if(i != 0 && Vector3.Distance(player[i].transform.position, this.transform.position) <= 
                             Vector3.Distance(player[i - 1].transform.position, this.transform.position))
                {
                    pPos = player[i].transform.position;
                }
            }

            if (mvRnd == 2)
            {
                int transRnd = Random.Range(0, 4);

                if (transRnd == 0)
                {
                    transform.Translate(-0.5f, 0, 0);
                }
                else if (transRnd == 1)
                {
                    transform.Translate(0.5f, 0, 0);
                }
                else if (transRnd == 2)
                {
                    transform.Translate(0, -0.5f, 0);
                }
                else
                {
                    transform.Translate(0, 0.5f, 0);
                }

            }

            else if (Mathf.Abs(pPos.x - pos.x) > Mathf.Abs(pPos.y - pos.y))
            {
                if (pPos.x - pos.x < 0)
                {
                    transform.Translate(-0.5f, 0, 0);
                }
                else
                {
                    transform.Translate(0.5f, 0, 0);
                }
            }

            else
            {

                if (pPos.y - pos.y < 0)
                {
                    transform.Translate(0, -0.5f, 0);
                }
                else
                {
                    transform.Translate(0, 0.5f, 0);
                }
            }
            cnt = 0;
        }
    }
}
