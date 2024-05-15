using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Text txt_GoalsRight, txt_GoalsLeft, txt_timeMatch;
    public int number_GoalsRight, number_GoalsLeft;
    public bool isScore, EndMatch;
    public float timeMatch;
    private GameObject _ball, _AI, _Player;

    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        number_GoalsRight = 0;
        number_GoalsLeft = 0;
        timeMatch = 20;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _AI = GameObject.FindGameObjectWithTag("AI");
        _Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(BeginMatch());
    }

    // Update is called once per frame
    void Update()
    {
        txt_GoalsLeft.text = number_GoalsLeft.ToString();
        txt_GoalsRight.text = number_GoalsRight.ToString();
        txt_timeMatch.text = timeMatch.ToString();
    }

    IEnumerator BeginMatch()
    {
        
        while(true)
        {
            yield return new WaitForSeconds(1f);
            if (timeMatch > 0)
            {
                timeMatch--;
                
            }
            else
            {
                //StartCoroutine(WaitEndGame());
                EndMatch = true;
                break;
            }
        }
    }

    public void ContinueMatch(bool winPlayer)
    {
        StartCoroutine(WaitContinueMatch(winPlayer));
    }
    
    IEnumerator WaitContinueMatch(bool winPlayer)
    {
        yield return new WaitForSeconds(2f);
        isScore = false;
        if(EndMatch == false)
        {
            _ball.transform.position = new Vector3(0, 0, 0);
            _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            _AI.transform.position = new Vector3(-6, 0, 0);
            _Player.transform.position = new Vector3(6, 0, 0);
            if(winPlayer == true)
            {
                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 200));
            }
            else
            {
                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 200));
            }
        }
    }

    IEnumerator WaitEndGame()
    {
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene("EndGame");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
