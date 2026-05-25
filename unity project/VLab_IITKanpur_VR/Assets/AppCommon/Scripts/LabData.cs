// Developed by Tushar. All Rights Reserved (c) VLab IIT Kanpur
// Description: Scrptable Object for containing Experiment data.

using UnityEngine;

[CreateAssetMenu(fileName = "LabData", menuName = "ScriptableObjects/LabData", order = 1)]
public class LabData : ScriptableObject
{
    public string heading;
    [TextArea(3,4)]
    public string details;
    public Sprite thumbnail;

    public string sceneName;
}
