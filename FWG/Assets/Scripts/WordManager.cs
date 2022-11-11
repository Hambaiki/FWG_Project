using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WordManager : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public List<GameObject> words;
    public Transform canvas;

    public TextMeshProUGUI status;
    public TextMeshProUGUI playerName;
    
    public float wordDelay = 3f;
	private float nextWordTime = 0f;

    public float fallSpeed = 1f;

    private bool editNotificaiton = false;
    private GameObject removingObject;
    
    public bool timeOut = false;
    public bool gameOver = false;
    public bool gamePause = false;

    void Start()
    {
        playerName.text = Welcome.playerName;
    }

    void Update()
    {
        // Game pausing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePause == false)
            {
                gamePause = true;
                Timer.timerIsRunning = false;
            } else
            {
                gamePause = false;
                Timer.timerIsRunning = true;
            }
        }

        // Updating word transfrom
        if (gameOver == false && gamePause == false) {
            if (Time.time >= nextWordTime)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-100f, 100f), 0f);
                
                GameObject newObject = Instantiate(prefab, spawnPoint.position + randomOffset, Quaternion.identity, canvas);
                TextMeshProUGUI word = newObject.GetComponent<TextMeshProUGUI>();
                word.text = WordRandomizer();
                words.Add(newObject);

                nextWordTime = Time.time + wordDelay;
                wordDelay *= .99f;
            }

            // Check for pending changes be for iterating the upcoming foreach loop
            if (editNotificaiton == true)
            {
                Destroy(removingObject);
                
                words.Remove(removingObject);
                removingObject = null;
                editNotificaiton = false;    
            }

            // When the time runs out, destroy all the entity first. Then load next scene after that.
            if (timeOut == false)
            {
                foreach (GameObject word in words)
                {
                    word.transform.Translate(0f, -fallSpeed * Time.deltaTime*30, 0f);
                }
            } else
            {
                foreach (GameObject word in words)
                {
                    GameObject[] destroyObject;
                    destroyObject = GameObject.FindGameObjectsWithTag("Word");
                    foreach (GameObject oneObject in destroyObject)
                    {
                        Destroy(oneObject);
                    }
                    gameOver = true;
                    status.text = "TIME IS UP!";
                }
            }
        } else if (gameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void FindWord(string input)
    {
        foreach (GameObject member in words)
        {
            TextMeshProUGUI word = member.GetComponent<TextMeshProUGUI>();
            if (word.text == input)
            {
                status.text = MotivationWordRandomizer();
                Debug.Log(input + " exists!");
                RemoveWord(member);

                InputManager inputManager = gameObject.GetComponent<InputManager>();
                inputManager.updateScore();

                return;
            }
        }
        status.text = input + " DOES NOT EXISTS!";
        Debug.Log(input + " not found!");
    }

    public void RemoveWord(GameObject word)
    {
        removingObject = word;
        editNotificaiton = true;
    }

    public string WordRandomizer()
    {
        int randomIndex = Random.Range(0, wordList.Length);
		string randomWord = wordList[randomIndex];

        // We may choose different wordList for different level of difficulties...

        return randomWord;
    }
    
    public string MotivationWordRandomizer()
    {
        int randomIndex = Random.Range(0, motivationWordList.Length);
		string randomWord = motivationWordList[randomIndex];

        return randomWord;
    }

    private static string[] motivationWordList = {  "Well done!", "Nice!", "Awesome!", "Great!", "You're doing great!", "Cool!" };

    private static string[] wordList = {   "sidewalk", "robin", "three", "protect", "periodic",
									"somber", "majestic", "jump", "pretty", "wound", "jazzy",
									"memory", "join", "crack", "grade", "boot", "cloudy", "sick",
									"mug", "hot", "tart", "dangerous", "mother", "rustic", "economic",
									"weird", "cut", "parallel", "wood", "encouraging", "interrupt",
									"guide", "long", "chief", "mom", "signal", "rely", "abortive",
									"hair", "representative", "earth", "grate", "proud", "feel",
									"hilarious", "addition", "silent", "play", "floor", "numerous",
									"friend", "pizzas", "building", "organic", "past", "mute", "unusual",
									"mellow", "analyse", "crate", "homely", "protest", "painstaking",
									"society", "head", "female", "eager", "heap", "dramatic", "present",
									"sin", "box", "pies", "awesome", "root", "available", "sleet", "wax",
									"boring", "smash", "anger", "tasty", "spare", "tray", "daffy", "scarce",
									"account", "spot", "thought", "distinct", "nimble", "practise", "cream",
									"ablaze", "thoughtless", "love", "verdict", "giant"    };
}
