using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSwitch : MonoBehaviour
{
    [SerializeField]
    public int characterIndex = 0;
    public List<GameObject> characters;
    public List<Image> characterIconsSelected;


    private void Start()
    {
        if (characters.Count > 0)
        {
            SwitchCharacter(characterIndex);
        }
    }
    public void SwitchCharacter(int index)
    {
        if (index < 0 || index >= characters.Count)
        {
            Debug.LogWarning("Character index out of range.");
            return;
        }

        for (int i = 0; i < characters.Count; i++)
        {
            var IconsSelect = characterIconsSelected[i];
            if(IconsSelect != null)
            {
                IconsSelect.enabled = (i == index);
            }

            var movementScript = characters[i].GetComponent<PlayerMovement>();
            if (movementScript != null)
            {
                movementScript.enabled = (i == index);
            }
        }

        characterIndex = index;
    }
    //char ke kanan
    public void NextCharacter()
    {
        int nextIndex = (characterIndex + 1) % characters.Count;
        SwitchCharacter(nextIndex);
    }
    //char ke kiri
    public void PreviousCharacter()
    {
        int previousIndex = (characterIndex - 1 + characters.Count) % characters.Count;
        SwitchCharacter(previousIndex);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            PreviousCharacter();
        }
    }
}
