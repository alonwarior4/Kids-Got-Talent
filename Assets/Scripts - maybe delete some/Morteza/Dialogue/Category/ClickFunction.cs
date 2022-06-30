using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClickFunction : MonoBehaviour
{

    [SerializeField] int thisIndex;
    

    private void OnMouseDown()
    {
        Ca_Questions.Instance.ClickOptions(thisIndex);
    }

}
