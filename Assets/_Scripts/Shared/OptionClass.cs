using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionClass : MonoBehaviour
{
    public string title;
    public string body;
    public string[] effects;

    public OptionClass(string title, string body, string[] effects)
    {
        this.title = title;
        this.body = body;
        this.effects = effects;
    }
}
