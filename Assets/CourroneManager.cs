using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CourroneManager : MonoBehaviour
{
    public enum COLORS
    {
        GREEN,
        BLUE,
        RED,
        YELLOW,
        WHITE,
        PURPLE
    };
    List<COLORS> ActualColorList = new List<COLORS>();
    List<COLORS> PlayerColorListColorList = new List<COLORS>();
    [SerializeField] int minimalTurn = 8;
    [SerializeField]  List<Button> ButtonList;

    // Start is called before the first frame update
    void Start()
    {
        ActualColorList.Add((COLORS)Random.Range(0,6));
        StartCoroutine(StartSimonSequence());
    }
    public void AddColor(COLORS newColor)
    {
        PlayerColorListColorList.Add(newColor);
        if (PlayerColorListColorList[PlayerColorListColorList.Count - 1] != ActualColorList[PlayerColorListColorList.Count - 1])
        {

            Debug.LogError("The player failed the game.");
            ActualColorList.Clear();
            PlayerColorListColorList.Clear();
            return;
        }
        if (PlayerColorListColorList.Count == ActualColorList.Count)
            RestartSimon();
    }
    
    void RestartSimon()
    {
        ActualColorList.Add((COLORS)Random.Range(0, 6));
        PlayerColorListColorList.Clear();
        if (ActualColorList.Count > minimalTurn)
        {
            Debug.Log("VICTORY");
            return;
        }
        StartCoroutine(StartSimonSequence());
    }



    IEnumerator StartSimonSequence()
    {
        for (int i = 0; i < ActualColorList.Count; i++)
        {
            ButtonList[(int)ActualColorList[i]].image.color = new Color(ButtonList[(int)ActualColorList[i]].image.color.r, ButtonList[(int)ActualColorList[i]].image.color.g, ButtonList[(int)ActualColorList[i]].image.color.b, 1);
            yield return new WaitForSeconds(0.5f);
            ButtonList[(int)ActualColorList[i]].image.color = new Color(ButtonList[(int)ActualColorList[i]].image.color.r, ButtonList[(int)ActualColorList[i]].image.color.g, ButtonList[(int)ActualColorList[i]].image.color.b, 0.25f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
