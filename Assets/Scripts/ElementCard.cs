using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewElementCard", menuName = "ElementCard")]
public class ElementCard : ScriptableObject {

    public new string name;

    public Sprite icon;

    public string tag;

}
