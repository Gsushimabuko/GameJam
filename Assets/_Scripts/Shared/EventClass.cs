using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventClass
{
    public int ID;
    public string title;
    public string body;
    public string category;
    public float tsocial;
    public float trsocial;
    public int tree;
    public int waitTime;

    #region Option 1
    public string title1;
    public string body1;

    public float work1;
    public float health1;
    public float money1;
    public float social1;
    public float hapiness1;
    public string results1;
    #endregion

    #region Option 2
    public string title2;
    public string body2;

    public float work2;
    public float health2;
    public float money2;
    public float social2;
    public float hapiness2;
    public string results2;
    #endregion

    #region Option 3
    public string title3;
    public string body3;

    public float work3;
    public float health3;
    public float money3;
    public float social3;
    public float hapiness3;
    public string results3;
    #endregion

    public string[] options;
    public string unlocks;

    public void Print()
    {
        Debug.Log(ID + " - " + title);
    }
}
