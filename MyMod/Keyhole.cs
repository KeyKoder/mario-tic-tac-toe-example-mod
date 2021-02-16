using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Keyhole : Hazard, IPointerClickHandler {

    new void Start() {
        base.Start();
        tag = "Passthrough";
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        Token.blocked = true;
        SFXManager.PlayFromWav(ModManager.GetAssetInMod(ModGameController.ModName, "keyhole.wav", "Hazards"));
        var mario = Instantiate((GameObject)Utils.LoadFromAssetBundle(ModGameController.ModName, "Speedrunner Mario"), new Vector2(-1000,1000), Quaternion.identity, GameData.data.canvas.transform);
        DOTween.To(() => transform.localScale, x => transform.localScale = x, Vector3.one * 3, 1.5f);
        mario.transform.localScale = new Vector3(-1, 1, 1);
        StartCoroutine(SummonMario(mario));
        foreach (Token token in FindObjectsOfType<Token>()) {
            var dummy = Instantiate(token.gameObject, token.transform.position, Quaternion.identity, GameData.data.foregroundEffects.transform);
            token.containsToken = false;
            token.playerIndex = -1;
            Destroy(dummy.GetComponent<Token>());
            DOTween.To(() => dummy.transform.position, x => dummy.transform.position = x, transform.position, 7.5f).SetEase(Ease.OutExpo);
            dummy.transform.DORotate(Vector3.forward * (360 + 180*UnityEngine.Random.Range(1f,4f)), 7.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutExpo);
            DOTween.To(() => dummy.transform.localScale, x => dummy.transform.localScale = x, Vector3.zero, 7.5f).SetEase(Ease.OutExpo);
        }
        StartCoroutine(EndKeyhole());
    }

    IEnumerator EndKeyhole() {
        yield return new WaitForSeconds(7.5f);
        DOTween.To(() => transform.localScale, x => transform.localScale = x, Vector3.zero, 1.5f).OnComplete(() => {
            Destroy(gameObject);
            Token.blocked = false;
        });
    }

    IEnumerator SummonMario(GameObject mario) {
        yield return new WaitForSeconds(4.5f);
        SFXManager.PlayFromWav(ModManager.GetAssetInMod(ModGameController.ModName, "mariofall.wav", "Generic Stuff"));
        DOTween.To(() => mario.transform.position, x => mario.transform.position = x, transform.position, 3f);
        mario.transform.DORotate(Vector3.forward * (360 + 180 * UnityEngine.Random.Range(4f, 8f)), 3f, RotateMode.LocalAxisAdd);
        DOTween.To(() => mario.transform.localScale, x => mario.transform.localScale = x, Vector3.zero, 3f).OnComplete(() => {
            Destroy(mario);
        });
    }

    public new void Kick(bool makeEffect) { }
}