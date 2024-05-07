using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReadQuestionnaireData : MonoBehaviour
{
    // 1. What file do we need to read?
    public string fileName; // NB! no extension, path relative to Resources folder or no path at all.

    // Create a list
    public List<QuestionnaireEntry> data;
    public UnityEvent finishedReadingData;

    private void Start() {
        ParseData();
    }

    private void ParseData() {
        // 2. Extract all the text from the file.
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        string fileContent = textAsset.text;
        Debug.Log(fileContent);

        // 3. Read the information:

        // Initialize the list (by default the list is null)
        data = new List<QuestionnaireEntry>();

        //      3.1. Each line - an entry;
        string[] fileLines = fileContent.Split('\n');
        // Loop over every line.
        for (int i = 1; i < fileLines.Length; i++) {
            //      3.2. Within each line ; separated values.
            string[] rawValues = fileLines[i].Split(';');
            //Debug.Log(rawValues[0]);

            //      3.3. Put it in a workable format.
            QuestionnaireEntry newEntry
                = new QuestionnaireEntry(rawValues);
            // Add the entry to the list.
            data.Add(newEntry);
        }

        if (finishedReadingData != null) {
            finishedReadingData.Invoke();
        }
    }
}
