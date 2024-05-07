using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[System.Serializable]
public class QuestionnaireEntry
{
    public int question1;
    public int question2;
    public int question3;
    public int question4;

    // Enumaerate the options
    public enum QuestionType { 
        Question1,
        Question2,
        Question3,
        Question4
    }

    // Constructor - a function that returns an instance of the class.
    public QuestionnaireEntry(string[] rawValues) {
        // int - Convert.ToInt32()
        // float - Convert.ToSingle()
        // set all the information
        question1 = Convert.ToInt32(rawValues[0]);
        question2 = Convert.ToInt32(rawValues[1]);
        question3 = Convert.ToInt32(rawValues[2]);
        question4 = Convert.ToInt32(rawValues[3]);
    }
}
