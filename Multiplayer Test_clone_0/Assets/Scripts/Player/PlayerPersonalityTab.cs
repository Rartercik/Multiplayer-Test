using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerPersonalityTab : MonoBehaviour
{
    public void Initialize(string name)
    {
        GetComponent<Text>().text = name;
        gameObject.SetActive(true);
    }
}
