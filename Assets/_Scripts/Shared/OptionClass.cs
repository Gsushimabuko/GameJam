using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionClass
{
    public int ID;
    public string title;
    public string body;

    public float work;
    public float health;
    public float money;
    public float social;
    public float hapiness;

    public string result;

    public OptionClass(string title, string body, float work, float health, float money, float social, float hapiness)
    {
        this.title = title;
        this.body = body;
        this.work = work;
        this.health = health;
        this.money = money;
        this.social = social;
        this.hapiness = hapiness;
    }
    
    public OptionClass()
    {

    }
}
