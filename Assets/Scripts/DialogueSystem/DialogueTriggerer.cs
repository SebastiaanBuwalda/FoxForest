using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Story"&&collision.gameObject.GetComponent<StoryElement>().alreadyTriggered == false)
        {
            collision.gameObject.GetComponent<StoryElement>().TriggerDialogue();
        }
    }
}
