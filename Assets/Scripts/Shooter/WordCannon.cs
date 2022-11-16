using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCannon : Weapon
{
    // Get the file input as words
    public override void Reload()
    {
        base.Reload();
        string newAmmo = FileManager.ReadFile(GameManager.AMMO_FILE_PATH);

        string aux = newAmmo;
        char spaceCharacter = ' ';
        char lineCharacter = '\n';
        while (aux.Contains(spaceCharacter) || aux.Contains(lineCharacter))
        {
            int separatorIndex = aux.IndexOf(spaceCharacter);
            if (separatorIndex == -1) separatorIndex = aux.IndexOf(lineCharacter);

            string word = aux.Substring(0, separatorIndex);
            ammo.Add(word);
            aux = aux.Substring(separatorIndex + 1, aux.Length - separatorIndex - 1);
        }

        ammo.Add(aux);
    }
}