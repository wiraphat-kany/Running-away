using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    
    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
  
   public void SelectCharacter()
   {
       UnityEngine.SceneManagement.SceneManager.LoadScene("SelectCharacter");
   }
   
   public void GamePlay()
   {
       UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
   }
   
   public void SettingResolution()
   {
       UnityEngine.SceneManagement.SceneManager.LoadScene("SettingResolution");
   }
   
   public void Character()
   {
       UnityEngine.SceneManagement.SceneManager.LoadScene("Char Selection");
   }
   
   public void Exit()
   {
       Application.Quit();
   }
   
   public void OnTriggerEnter2D()
   {
       
   }
   
}
   
