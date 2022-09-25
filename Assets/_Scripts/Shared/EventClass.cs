using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventClass
{
    public string ID;
    public string title;
    public string body;
    public string category;

    #region Option 1
    public string title1;
    public string body1;
    public string work1;
    public string health1;
    public string money1;
    public string social1;
    public string hapiness1;
    #endregion

    #region Option 2
    public string title2;
    public string body2;
    public string work2;
    public string health2;
    public string money2;
    public string social2;
    public string hapiness2;
    #endregion

    #region Option 3
    public string title3;
    public string body3;
    public string work3;
    public string health3;
    public string money3;
    public string social3;
    public string hapiness3;
    #endregion

    public string[] options;
    public string unlocks;

    public EventClass(string iD, string title, string body, string category, string title1, string body1, string work1, string health1, string money1, string social1, string hapiness1, string title2, string body2, string work2, string health2, string money2, string social2, string hapiness2, string title3, string body3, string work3, string health3, string money3, string social3, string hapiness3)
    {
        ID = iD;
        this.title = title;
        this.body = body;
        this.category = category;
        this.title1 = title1;
        this.body1 = body1;
        this.work1 = work1;
        this.health1 = health1;
        this.money1 = money1;
        this.social1 = social1;
        this.hapiness1 = hapiness1;
        this.title2 = title2;
        this.body2 = body2;
        this.work2 = work2;
        this.health2 = health2;
        this.money2 = money2;
        this.social2 = social2;
        this.hapiness2 = hapiness2;
        this.title3 = title3;
        this.body3 = body3;
        this.work3 = work3;
        this.health3 = health3;
        this.money3 = money3;
        this.social3 = social3;
        this.hapiness3 = hapiness3;
    }

    public void Print()
    {
        Debug.Log(ID + " - " + title);
    }
}
