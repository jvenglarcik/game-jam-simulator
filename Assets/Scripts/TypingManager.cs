﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TypingManager : MonoBehaviour
{
    public Line line;
    public Text cursor;
    public Text toType;
    public Text userInput;

    void Start()
    {
        cursor.text = "|";
        toType.text = line.text;
    }

    void Update()
    {
        string input = Input.inputString;

        // If we aren't typing, do nothing
        if (input.Equals(""))
        {
            return;
        }

        // If we are typing, check typed character
        string typing = "";
        char c = input[0];
        string typed = line.read(c);
        typing += typed + "\n";

        // If we typed the whole word
        if (typed.Equals(line.text))
        {
            Debug.Log("TYPED : " + line.text);
        }

        userInput.text = typing;
        cursor.text = "";
        for (int i = 1; i < typing.Length; i++)
        {
            cursor.text += " ";
        }
        cursor.text += "|";
    }
}

[System.Serializable]
public class Line
{
    public string text;
    public UnityEvent onTyped;
    string hasTyped;
    int curChar;

    public Line(string t)
    {
        text = t;
        onTyped = new UnityEvent();
        hasTyped = "";
        curChar = 0;
    }

    public string read(char c)
    {
        // backspace
        if (c.Equals('\b'))
        {
            if (curChar > 0)
            {
                curChar -= 1;
            }
            hasTyped = hasTyped.Substring(0, curChar);
        }
        // add characters
        else
        {
            curChar++;
            hasTyped += c;
        }
        return hasTyped;
    }
}