using UnityEngine;
using IUP.Toolkits.DialogueSystem;

public class DialogueSystemTest : MonoBehaviour
{
    private void Awake()
    {
        var a = new DialogueBoolVariable(true);
        var b = new DialogueBoolVariable(true);
        var isEqualTransitCondition = new TransitCondition<DialogueBoolVariable, bool>(a, b, a.IsEqual);
        var isNotEqualTransitCondition = new TransitCondition<DialogueBoolVariable, bool>(a, b, a.IsNotEqual);
        Debug.Log(isEqualTransitCondition.IsTrue());
        Debug.Log(isNotEqualTransitCondition.IsTrue());
    }
}
