using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{

    //===========References============
    [SerializeField] GameObject messagePrefab;
    [SerializeField] Transform messageParent;
    [SerializeField] GameObject newChatIcon;
    List<MessageData> messageDatas = new List<MessageData>();

    //=================================


    //==========Variables==============
    private MessageData currentMessageData;
    private int currentMessageIndex;


    //=================================

    void Awake()
    {
        // Load all MessageData assets from the "MessageData" folder within Resources
        MessageData[] loadedMessageDatas = Resources.LoadAll<MessageData>("MessageData");
        
        // Add the loaded MessageData assets to the messageDatas list
        messageDatas.AddRange(loadedMessageDatas);
    }

    void Start()
    {
        Debug.Log("messageDatas[0].ToString()" + messageDatas[GameManager.Instance.currentLevelID].name);
        SetCurrentChat(messageDatas[GameManager.Instance.currentLevelID].name,0);
        
    }

    public void ToNextMessage()
    {
        if (currentMessageData.messageContentList.Count > currentMessageIndex + 1)
        {
            SetCurrentChat("none", currentMessageIndex + 1);
            SoundManager.Instance.PlaySFX("Click_OK");
            if(currentMessageData.messageContentList.Count == currentMessageIndex + 1)
            {
                newChatIcon.SetActive(false);
            }
        }
        else
        {
            SoundManager.Instance.PlaySFX("Click_Confirm");
            newChatIcon.SetActive(false);
        }


    }

    public void SetCurrentChat(string messageData, int index)
    {

        if (messageData != "none")
        {
            currentMessageData = FindMessageDataByName(messageData);
           
        }
        if(currentMessageData == null)
        {
            currentMessageData = messageDatas[0];
        }
        
        currentMessageIndex = index;
        InstantiateMessage();
    }

    public void InstantiateMessage()
    {
        
        var message = Instantiate(messagePrefab, new Vector3(0, 0, 0), Quaternion.identity, messageParent);

        //===========Image Null Bypass=======
        Transform ImageTransform = FindGrandchild(message.transform, "ImageContent");
        if(ImageTransform != null)
        {
            //Debug.Log("ImageContent found!");
            Image imageComponent = ImageTransform.GetComponent<Image>();
            
            
            if (imageComponent != null)
            {
                imageComponent.gameObject.SetActive(true);
                //Debug.Log("imageComponent Found!");
                Sprite sprite = currentMessageData.messageContentList[currentMessageIndex].sprite;
                if (sprite != null)
                {
                   message.GetComponent<Message>().currentSprite = sprite;
                }
                else
                {
                    ImageTransform.gameObject.SetActive(false);
                }
            }else{
                imageComponent.gameObject.SetActive(false);
            }
        }

        Transform textTransform = FindGrandchild(message.transform, "TextContent");
        if (textTransform != null)
        {
            //Debug.Log("TextContent found!");
            TextMeshProUGUI textComponent = textTransform.GetComponent<TextMeshProUGUI>();
            
            if (textComponent != null) {
                Debug.Log("textComponent Found!");
                string content = currentMessageData.messageContentList[currentMessageIndex].message;
                if (content != null)
                {
                    message.GetComponent<Message>().currentText = content;
                }
                else
                {
                    textTransform.gameObject.SetActive(false);
                }
            }
        }
  
    }

        // Function to find a MessageData by its name
    public MessageData FindMessageDataByName(string name)
    {
        // Use the Find method to search for the MessageData with the matching name
        MessageData foundMessageData = messageDatas.Find(messageData => messageData.name == name);
        
        // Return the found MessageData (or null if not found)
        return foundMessageData;
    }

    private Transform FindGrandchild(Transform parent, string grandchildName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == grandchildName)
            {
                return child;
            }
            Transform grandchild = FindGrandchild(child, grandchildName);
            if (grandchild != null)
            {
                return grandchild;
            }
        }
        return null; // Grandchild not found
    }
}  
