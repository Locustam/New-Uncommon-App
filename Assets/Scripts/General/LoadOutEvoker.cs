using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOutEvoker : MonoBehaviour
{
    [SerializeField]GameManager gameManager;

    void EvokeWinorNot()
    {
        gameManager.WinOrNot();
    }
}
