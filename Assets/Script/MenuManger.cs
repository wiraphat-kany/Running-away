using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManger : MonoBehaviour
{
    [SerializeField] private GameObject[] character;

    private int characterIndex;
   
    public void ChanegeCharacter(int index)
    {
        for (int i = 0; i < character.Length; i++)
        {
            character[i].SetActive(false);
        }
        this.characterIndex = index;
            
        character[index].SetActive(true); 
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);

        PlayerPrefs.SetInt("CharacterIndex",characterIndex);
    }
}
