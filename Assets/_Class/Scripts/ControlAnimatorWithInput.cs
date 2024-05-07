using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ControlAnimatorWithInput : MonoBehaviour
{
    // 1. What is the animator that we want to control?
    private Animator animator;
    public string animatorTimeParameterName;
    // 2. What is the input we are looking for?
    public InputActionReference scrollAction;
    public float minScrollValue, maxScrollValue;
    // 4. Trigger changes in editor on animation end.
    public UnityEvent onAnimationFinished;

    private float totalScrollAmount;

    private void Start() {
        animator = gameObject.GetComponent<Animator>();
        // 3. Configure the input to do the following:
        scrollAction.action.Enable();
        // started      - start of interaction
        // performed    - every frame after
        // canceled     - end of interaction
        // Add listener function to the callback.
        scrollAction.action.performed += ActionPerformed;
        totalScrollAmount = minScrollValue;
    }
    private void OnDestroy() {
        // When object is destroyed - make sure to unsubscribe
        scrollAction.action.performed -= ActionPerformed;
        // And disable the action to not waste resources.S
        scrollAction.action.Disable();
    }
    // A function that will be called when action is performed
    private void ActionPerformed(InputAction.CallbackContext obj) {
        //Debug.Log("Scrolling!");

        //      3.1. Read the scroll value
        float rawScrollValue = obj.ReadValue<float>();
        //Debug.Log(rawScrollValue);

        // Accumulate scrolling amount
        totalScrollAmount = totalScrollAmount + rawScrollValue;

        // if value went bellow 0 - force it back to be 0
        if (totalScrollAmount < minScrollValue) { 
            totalScrollAmount = minScrollValue;
        }
        //Debug.Log(totalScrollAmount);

        // Go from arbitrary range of scroll values to 0 to 1 range.
        float time = Mathf.InverseLerp(minScrollValue, maxScrollValue, totalScrollAmount);
        Debug.Log(time);

        //      3.2. Pass the value to the animator.
        animator.SetFloat(animatorTimeParameterName, time);
    }

    // A function to be triggered by the animation at the end of it.
    public void OnAnimationFinished() {
        //Debug.Log("Finished Animating");
        scrollAction.action.performed -= ActionPerformed;
        // check if callback was assigned
        if (onAnimationFinished != null) {
            onAnimationFinished.Invoke();
        }
    }
}
