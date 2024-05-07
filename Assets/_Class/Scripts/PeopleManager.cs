using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PeopleManager : MonoBehaviour
{
    // 1. Which are all the people?
    public List<PersonData> people;
    // 2. What are the connections?
    public List<ConnectionData> connections;

    public GameObject lineVisual;

    private void Start() {
        SetUpConnectionVisuals();
    }
    public void SetUpConnectionVisuals() {
        // 3. for every connection
        foreach (ConnectionData connection in connections) {
            List<Vector3> peopleVisualPositions = new List<Vector3>();
            for (int i = 0; i < connection.peopleName.Count; i++) {
                //      3.1. find a person with first name in collection
                string nameA = connection.peopleName[i];
                Debug.Log(nameA);
                // find the first occurence of the person with matching name.
                PersonData personA = people.First(person => person.personName == nameA);
                peopleVisualPositions.Add(personA.personVisual.transform.position);
                //      3.2. find a person with next name
                int nextPersonI = (i + 1) % connection.peopleName.Count;
                string nameB = connection.peopleName[nextPersonI];
                Debug.Log(nameB);
                PersonData personB = people.First(person => person.personName == nameB);
                peopleVisualPositions.Add(personB.personVisual.transform.position);
            }
            //      3.3. draw a line from position of first person to next
            GameObject item = Instantiate(lineVisual);
            LineRenderer line = item.GetComponent<LineRenderer>();
            line.positionCount = peopleVisualPositions.Count;
            line.SetPositions(peopleVisualPositions.ToArray());
        }
    }
}
