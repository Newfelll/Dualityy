using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> prefab;
    private GameObject[] spawnList=new GameObject[5];
    private int spawnNumber;
    [SerializeField] private int spawnDistanceInterval=6;
    private Vector2 spawnPos =new Vector2(10,0);
    private Vector2 falan = new Vector2(5, 0);
    
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI  scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject DeathScreen;

    private float score=0;
    private int highScore=0;
    private int currentRunScore;
    private float pollingTime=1f;
    private float time;
    private int frameCount;
    [SerializeField]private float repeatRate = 4;
    [SerializeField]public static bool isPlaying = true;
    
    


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        Application.targetFrameRate = 60;
        InvokeRepeating("SpawnBubble", 2, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
       
       

        if (isPlaying) { 
        score += Time.deltaTime * MoveLeft.speed;
        scoreText.text = ((int)score).ToString();
                }
      
    }



    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        
    }


    void SpawnBubble()
    {   

        
        if (isPlaying)
        {

             for (int i=0; i<=4; i++)
             {
                 spawnNumber = Random.Range(0,4);

                 spawnList[i] = prefab[spawnNumber];               
                 

             }

             for(int i = 0; i <= 4; i++)               
             {
                if (Random.value < 0.5)
                {
                    spawnPos.x += (i+1) * spawnDistanceInterval;
                    spawnPos.y = -spawnList[i].transform.position.y;
                }
                else
                {
                    spawnPos.x += i * spawnDistanceInterval;
                    spawnPos.y = spawnList[i].transform.position.y;
                }

                 Instantiate(spawnList[i],spawnPos, spawnList[i].transform.rotation);

             }
            spawnPos.x = 10;

         /*   var spawnPre = prefab[Random.Range(0, 4)];

            if (Random.value < 0.5)
            {
                spawnPre.transform.position = new Vector2(spawnPre.transform.position.x, spawnPre.transform.position.y);
            }
            else
            {
                spawnPre.transform.position = new Vector2(spawnPre.transform.position.x, -spawnPre.transform.position.y);
            }

            Instantiate(spawnPre, (Vector2)spawnPre.transform.position+falan, spawnPre.transform.rotation);*/

        }
        
    }
   public  void GameOver()
    {
        isPlaying = false;
        currentRunScore = (int)score;

        
        if (currentRunScore > PlayerPrefs.GetInt("HighScore") )
        {

            PlayerPrefs.SetInt("HighScore", currentRunScore);
        }
        highScoreText.text = "High Score:" + PlayerPrefs.GetInt("HighScore");
        DeathScreen.SetActive(true);
        MoveLeft.speed = 5;

       
    }
    

    public  void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPlaying = true;
        
       

    }

}
