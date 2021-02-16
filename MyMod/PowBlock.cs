using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PowBlock : Item {

    public void Start() {
        needsEmptySpace = true;
    }
    public override void Use(int tile) {
        SFXManager.PlayFromWav(ModManager.GetAssetInMod(ModGameController.ModName, "powblock.wav", "Items"));
        foreach(Token token in FindObjectsOfType<Token>()) {
            token.Kick(true);
        }
        foreach (Hazard hazard in FindObjectsOfType<Hazard>()) {
            hazard.Kick(true);
        }
        StartCoroutine(Utils.Shake(GameData.data.canvas, 1.6f, new Vector2(8, 16)));
        StartCoroutine(Utils.DestroyIn(1.65f, gameObject));
    }
}