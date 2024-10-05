using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewMessageData", menuName = "Message Data", order = 1)]
public class MessageData : ScriptableObject
{
    public List<SingleMessage> messageContentList = new List<SingleMessage>();
}

[System.Serializable]
public class SingleMessage
{
    public string message;
    public Sprite sprite;
}