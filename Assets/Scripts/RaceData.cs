using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStudentRace", menuName = "Character Customization/Student Race", order = 0)]
public class RaceData : ScriptableObject
{
    public List<Sprite> ASpriteList;
    public List<Sprite> BSpriteList;
    public List<Sprite> CSpriteList;
    public List<Sprite> DSpriteList;
    public List<Sprite> ESpriteList;
    public List<Sprite> FSpriteList;
    public List<Sprite> GSpriteList;
    public List<Sprite> HSpriteList;
    public List<Sprite> ISpriteList;
    public List<Sprite> JSpriteList;
    public List<Sprite> KSpriteList;
    public List<Sprite> LSpriteList;
    public List<List<Sprite>> bodyPartList = new List<List<Sprite>>();
    // Add more as needed, such as hair, mouth, etc.

    public List<bindedBodyParts> bindedBodyParts;

    // You can also add fields for race-specific attributes if needed.
    public string raceName;
    public string raceDescription; // Description or other characteristics.

    public enum NationType
    {
        Unmeiia,
        Nystal,
        Tendiyu,
        Ovyeka,
        Gessurd,
        None
    }

    public NationType FirstNation;
    public NationType SecondNation;
    private void OnEnable()
    {
        bodyPartList = new List<List<Sprite>> { ASpriteList, BSpriteList, CSpriteList, DSpriteList, ESpriteList, FSpriteList, GSpriteList, HSpriteList, ISpriteList, JSpriteList, KSpriteList, LSpriteList };
    }

}



[System.Serializable]
public class bindedBodyParts
{
    public enum SpriteComponents
    {
        A,B,C,D,E,F,G,H,I,J,K,L,M,N
    }

    public SpriteComponents FirstSpriteComponent;
    public SpriteComponents SecondSpriteComponent;


}
