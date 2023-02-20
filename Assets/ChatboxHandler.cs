using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatboxHandler : MonoBehaviour
{

    public GameObject LXmarks;
    public GameObject RXmarks;

    public GameObject LTextBox;
    public GameObject RTextBox;

    public bool chatting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Chat box") {

            if (Random.Range(0, 4) == 0 && !collision.gameObject.GetComponent<ChatboxHandler>().chatting) {


                if (collision.transform.position.x > transform.position.x) // if the other guy is to the right 
                {

                    ChatRight();

                    collision.GetComponent<ChatboxHandler>().ChatLeft();

                } 
                else {

                    ChatLeft();

                    collision.GetComponent<ChatboxHandler>().ChatRight();

                }

            }
        }

    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(6);

        StopChatting();

    }

    public void ChatRight(){

        chatting = true;

        RTextBox.GetComponent<ChatboxFlicker>().Show();

        LXmarks.SetActive(true);


        StartCoroutine(Reset());

    }

    public void ChatLeft()
    {

        chatting = true;

        LTextBox.GetComponent<ChatboxFlicker>().Show();

        RXmarks.SetActive(true);

        StartCoroutine(Reset());


    }

    public void StopChatting() {

        LTextBox.GetComponent<ChatboxFlicker>().Hide();

        RTextBox.GetComponent<ChatboxFlicker>().Hide();

        chatting = false;

        RXmarks.SetActive(false);

        LXmarks.SetActive(false);


    }

}
