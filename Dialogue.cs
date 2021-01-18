using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public string[] sentences;
    public int index = -1;

    [SerializeField] TextMeshProUGUI textDisplay;

    public float TypeSpeed;

    NPCMovement NPC;

    IEnumerator Type()
    {
        foreach(char letter in sentences[index])
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(TypeSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StopAllCoroutines();
            ProcessPerc();
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;
            //End conversation
        }
    }

    public void NextSentence(NPCMovement N)
    {
        NPC = N;
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StopAllCoroutines();
            ProcessPerc();
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;
            N.enabled = true;
            //End conversation
        }
    }

    void ProcessPerc()
    {

        print(sentences[index]);
        if (sentences[index][0] == '%')
        {
            char c = sentences[index][1];
            print("FF");
            sentences[index] = sentences[index].Remove(0, 2);

            if(c == 'L')
            {
                NPC.Face("left");
            }else if(c == 'R')
            {
                NPC.Face("right");
            } else if(c == 'U')
            {
                NPC.Face("up");
            } else if(c == 'D')
            {
                NPC.Face("down");
            } else if(c == 'P')
            {
                NPC.Face(GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().position);
            }

        }

    }
}
