using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClass
{
    public string ID;
    public string title;
    public string body;
    public string[] options;
    public string unlocks;

    public EventClass(string iD, string title, string body, string[] options, string unlocks)
    {
        ID = iD;
        this.title = title;
        this.body = body;
        this.options = options;
        this.unlocks = unlocks;
    }
}
