using UnityEngine;
using UnityEngine.UI;

public class petMods : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public RawImage characterDisplay;

    public Texture2D character1;
    public Texture2D character2;
    public Texture2D character3;
    public Texture2D character4;

    public void SetCharacter1()
    {
        characterDisplay.texture = character1;
    }

    public void SetCharacter2()
    {
        characterDisplay.texture = character2;
    }

    public void SetCharacter3()
    {
        characterDisplay.texture = character3;
    }

    public void SetCharacter4()
    {
        characterDisplay.texture = character4;
    }
}
