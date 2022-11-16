using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterGun : Weapon
{
    // Get the file input as characters.
    public override void Reload()
    {
        base.Reload(); 
        string newAmmo = FileManager.ReadFile(GameManager.AMMO_FILE_PATH);
        char[] aux = newAmmo.ToCharArray();

        foreach (char letter in aux)
        {
            if (letter != ' ' && letter != '\n') ammo.Add(letter.ToString());
        }
    }
}
