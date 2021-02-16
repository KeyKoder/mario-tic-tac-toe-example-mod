using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ModGameController : MonoBehaviour {
    int sCounter = 0;
    int repetitions = 0;

    public static string ModName = "Example-Random Mod";

    public void Start() {
        MainMenuController.MainMenuLoop += MainMenuLoop;
    }

    public void MainMenuLoop() {
        if (Input.GetKeyDown(KeyCode.S)) {
            if (sCounter == 5) {
                if(repetitions == 3) {
                    SFXManager.PlayFromWav(ModManager.GetAssetInMod(ModName, "spaghet.wav", "Generic Stuff"));
                    repetitions = 0;
                } else {
                    SFXManager.PlayFromWav(ModManager.GetAssetInMod(ModName, "spaghetti.wav", "Generic Stuff"));
                }
                sCounter = 0;
                repetitions++;
            }
            sCounter++;
        }
    }
}