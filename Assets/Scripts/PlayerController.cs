using JetBrains.Annotations;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    GameObject sBar;

    [SerializeField]
    AudioClip loot;
    [SerializeField]
    AudioClip hit;
    bool isStack;

    AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sBar = GameObject.Find("ScoreBar");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 5.0f && (Input.GetKeyDown("w")))
        {
            transform.Translate(0, 0.5f,0);
        }
        else if (transform.position.x > -9.0f &&(Input.GetKeyDown("a")))
        {
            transform.Translate(-0.5f, 0, 0);
        }
        else if (transform.position.y > -5.0f && (Input.GetKeyDown("s")))
        {
            transform.Translate(0, -0.5f, 0);
        }
        else if (transform.position.x < 9.0f && (Input.GetKeyDown("d")))
        {
            transform.Translate(0.5f, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.GetComponent<PlayerController>().enabled == true)
        {
            if (other.CompareTag("Coin"))
            {
                if (isStack)
                {
                    GameController.score++;
                    sBar.transform.position += new Vector3(0.054f, 0, 0);
                }
                else
                {
                    GameController.score += 5;
                    sBar.transform.position += new Vector3(0.27f, 0, 0);
                }

                Destroy(other.gameObject);
                audioSource.PlayOneShot(loot, 0.8f);
            }
            else if (other.CompareTag("Enemy"))
            {
                GameController.score /= 2;
                sBar.transform.position = new Vector3(GameController.score * 0.054f - 17.9f, 5.0f, 0);
                Destroy(other.gameObject);
                audioSource.PlayOneShot(hit, 0.3f);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isStack = true;
        }
    }
}
