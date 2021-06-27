using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameScript : MonoBehaviour
{
   private bool NewSession;
    private float PrBar;
    private bool ProgressBar;
    public GameObject LoadingBar;
    public GameObject LoadingBar2;
    private bool RestartTaped;
    private float restartColor;
    public GameObject restart;
    private bool RestartReady;
    private float FadeOut;
    public GameObject FadeIn;
    private bool EndGame;
    public GameObject Confetti;
    int indexofbotton;
    public int CurrentLevel;
    public GameObject[] windows2;
    public GameObject[] windows3;
    public QuestionList[] questions;
    public QuestionList[] questions2;
    public QuestionList[] questions3;
    public TextMeshProUGUI questioninscene;
    public GameObject[] answersGameObjects;
    public QuestionList crntQ;
    public List<object> qList;
    public List<object> qList2;
    public List<object> qList3;
    private int randQ;
    private int randQ2;
    private int randQ3;
    private void Start()
    {
        CurrentLevel = 1;
        qList = new List<object>(questions);
        qList2 = new List<object>(questions2);
        qList3 = new List<object>(questions3);
        QuestionGenerate();
      
    }
    private void Update()
    {
        if (CurrentLevel == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                windows2[i].SetActive(false);
                windows3[i].SetActive(false);
            }
        }
        if (CurrentLevel == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                windows2[i].SetActive(true);
            }
        }

        if (CurrentLevel == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                windows3[i].SetActive(true);
            }
        }
        if(EndGame == true)
        {
            if (FadeOut < 0.7f)
            {
                FadeOut += 0.001f;
            }
            FadeIn.SetActive(true);
            FadeIn.GetComponent<Image>().color = new Color(0f, 0f, 0f, FadeOut);
           
        }
        if (RestartTaped == true)
        {
            if (FadeOut < 1f)
            {
                FadeOut += 0.01f;
            }
            
            FadeIn.GetComponent<Image>().color = new Color(0f, 0f, 0f, FadeOut);
            
        }


        if (RestartReady == true)
        {
           
            if (restartColor < 1f)
            {
                restartColor += 0.01f;
            }
            
            restart.GetComponent<Image>().color = new Color(1f, 1f, 1f, restartColor);
        }
        if (ProgressBar == true)
        {

            if (PrBar <= 1f)
            {
                PrBar += 0.001f;
            }
            if (PrBar >= 1f)
            {
                CurrentLevel = 1;
                QuestionGenerate();
            }

                LoadingBar2.GetComponent<Image>().fillAmount = PrBar;
        }

        if(PrBar >= 1)
        {
            RestartReady = false;
            ProgressBar = false;
            LoadingBar.SetActive(false);
            NewSession = true;
            RestartTaped = false;
            EndGame = false;
            PrBar = 0;
            restartColor = 0;
        }
        if(NewSession == true)
        {
            if (FadeOut >= 0f)
            {
                FadeOut -= 0.001f;
            }
            else
            {

                FadeIn.SetActive(false);
                NewSession = false;
                
            }

            FadeIn.GetComponent<Image>().color = new Color(0f, 0f, 0f, FadeOut);
        }

    }

    public void QuestionGenerate()
    {
        if (CurrentLevel == 1)
        {

            if (qList.Count > 0)
            {
                randQ = Random.Range(0, qList.Count);
                crntQ = qList[randQ] as QuestionList;
                questioninscene.text = crntQ.question;
                List<Sprite> answers = new List<Sprite>(crntQ.answers);
                for (int i = 0; i < crntQ.answers.Length; i++)
                {
                    int rand = Random.Range(0, answers.Count);
                    answersGameObjects[i].GetComponent<Image>().sprite = answers[rand];
                    answers.RemoveAt(rand);
                }
            }
            else
            {
                Debug.Log("вы прошли игру");
            }
        }
        if (CurrentLevel == 2)
        {
           
                randQ2 = Random.Range(0, qList2.Count);
                crntQ = qList2[randQ2] as QuestionList;
                questioninscene.text = crntQ.question;
                List<Sprite> answers = new List<Sprite>(crntQ.answers);
                for (int i = 0; i < crntQ.answers.Length; i++)
                {
                    int rand = Random.Range(0, answers.Count);
                    answersGameObjects[i].GetComponent<Image>().sprite = answers[rand];
                    answers.RemoveAt(rand);
                }
            
           
        }
        if (CurrentLevel == 3)
        {
            
                randQ3 = Random.Range(0, qList3.Count);
                crntQ = qList3[randQ3] as QuestionList;
                questioninscene.text = crntQ.question;
                List<Sprite> answers = new List<Sprite>(crntQ.answers);
                for (int i = 0; i < crntQ.answers.Length; i++)
                {
                    int rand = Random.Range(0, answers.Count);
                    answersGameObjects[i].GetComponent<Image>().sprite = answers[rand];
                    answers.RemoveAt(rand);
                }
            
        }
        if (CurrentLevel == 4)
        {
            EndGame = true;
            StartCoroutine(Restart());

        }
        else
        {
            EndGame = false;
        }

        }
    

    public void ansversbutton(int index)
    {
        
        if (answersGameObjects[index].GetComponent<Image>().sprite  == crntQ.answers[0])
        {
            answersGameObjects[index].GetComponent<Animator>().SetBool("IsRight", true);
            answersGameObjects[index].GetComponent<Animator>().SetBool("IsWrong", false);
            Confetti.transform.position = answersGameObjects[index].transform.position ;

            StartCoroutine(Particle());
            Confetti.SetActive(true);
            StartCoroutine(Right());
            
        }
        else
        {
          
            answersGameObjects[index].GetComponent<Animator>().SetBool("IsWrong", true);
            StartCoroutine(Wrong());
            Debug.Log("не Правильно");
        }
        indexofbotton =index;
    }
    IEnumerator Wrong()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log("OFF");
  
            answersGameObjects[indexofbotton].GetComponent<Animator>().SetBool("IsWrong", false);
        
    }
    IEnumerator Right()
    {
        yield return new WaitForSeconds(0.5f);
        answersGameObjects[indexofbotton].GetComponent<Animator>().SetBool("IsRight", false);
        CurrentLevel++;
        Debug.Log("Правильно");
        if (CurrentLevel == 1)
        {
            qList.RemoveAt(randQ);
        }
        if (CurrentLevel == 2)
        {
            qList2.RemoveAt(randQ2);
        }
        if (CurrentLevel == 3)
        {
            qList3.RemoveAt(randQ3);
        }

        QuestionGenerate();
     

    }
    IEnumerator Particle()
    {
        yield return new WaitForSeconds(1f);
        Confetti.SetActive(false);

    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);
        restart.SetActive(true);
        RestartReady = true;
    }
    IEnumerator Bar()
    {
        yield return new WaitForSeconds(2f);
        LoadingBar.SetActive(true);
        ProgressBar = true;


    }

    public void RestartLevel()
    {
        restart.SetActive(false);
        RestartTaped = true;
        StartCoroutine(Bar());
        
    }

}
[System.Serializable]
public class QuestionList
{
    public string question;
    public Sprite[] answers;
}
