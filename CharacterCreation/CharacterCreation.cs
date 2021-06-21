using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] characterPrafabs;
    private GameObject[] characterGameObjects;
    private int selectIndex = 0;
    private int length;
    public InputField nameInput;

    void Start()
    {
        length = characterPrafabs.Length;
        characterGameObjects = new GameObject[length];
        for(int i = 0; i < length; i++)
        {
            characterGameObjects[i] = GameObject.Instantiate(characterPrafabs[i], transform.position, transform.rotation)as GameObject;
        }
        UpdateShowCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateShowCharacter()
    {
        characterGameObjects[selectIndex].SetActive(true);
        for(int i = 0; i < characterGameObjects.Length; i++)
        {
            if (i != selectIndex)
            {
                characterGameObjects[i].SetActive(false);
            }
        }
    }
    public void OnNextButton()
    {
        selectIndex++;
        selectIndex %= length;
        UpdateShowCharacter();
    }

    public void OnPreButton()
    {
        selectIndex--;
        if (selectIndex ==-1)
        {
            selectIndex = length - 1;
        }
        UpdateShowCharacter();
    }
    public void OnSubitButtonClick()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectIndex);
        PlayerPrefs.SetString("Name", nameInput.text);
        Application.LoadLevel(2);
    }
}
