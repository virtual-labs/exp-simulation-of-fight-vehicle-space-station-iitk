using UnityEngine;

public class button : MonoBehaviour
{
    public Color buttonColor = Color.green;

    void Start()
    {
        GetComponent<Renderer>().material.color = buttonColor;
    }

     public void test()
    {
        Debug.Log("Button pressed!");
        GetComponent<Renderer>().material.color = Color.red;
    }
}

