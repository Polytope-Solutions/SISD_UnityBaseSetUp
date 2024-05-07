using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualizeQuestionnaireData : MonoBehaviour
{
    // 1. Get a reference to the script reading data
    public ReadQuestionnaireData dataSource;
    //      Which of the Questions we are trying to visualize?
    public QuestionnaireEntry.QuestionType questionToVisualize;
    // 2. What is the prefab for each of the entries?
    public GameObject prefab;
    //      What are the color scheme for the visualization?
    public Gradient colorScheme;
    //      What is the range of values we are visualizing?
    public int minValue, maxValue;
    public float transparencyMultiplier;

    public void VisualizeData() {
        // 3. Loop over the full collection
        for (int i = 0; i < dataSource.data.Count; i++) {
            //      3.1. Create a copy of the prefab
            GameObject copy = Instantiate(prefab);
            copy.transform.SetParent(transform);
            copy.name = "Data-" + i;
            copy.transform.localPosition = Vector3.zero;
            //      3.2. Adjust the transparency
            float transparency = 1f / dataSource.data.Count;
            //      3.3. Colour the image based on the index.
            float gradientFactor = Mathf.InverseLerp(0f, dataSource.data.Count - 1, i);
            Color evaluatedColor = colorScheme.Evaluate(gradientFactor);
            evaluatedColor.a = transparency * transparencyMultiplier;
            //      3.4. Adjust the fill amount
            int value = 0;
            // If we are visualizing Question1 access it's value.
            if (questionToVisualize == QuestionnaireEntry.QuestionType.Question1) { 
                value = dataSource.data[i].question1;
            }
            else if (questionToVisualize == QuestionnaireEntry.QuestionType.Question2) {
                value = dataSource.data[i].question2;
            }
            else if (questionToVisualize == QuestionnaireEntry.QuestionType.Question3) {
                value = dataSource.data[i].question3;
            }
            else if (questionToVisualize == QuestionnaireEntry.QuestionType.Question4) {
                value = dataSource.data[i].question4;
            }
            float fillAmount = Mathf.InverseLerp(minValue, maxValue, value);

            Image image = copy.GetComponent<Image>();
            image.color = evaluatedColor;
            image.fillAmount = fillAmount;
        }
    }
}
