using UnityEngine;
using System.Collections;

public class HideAllTextureConversions : MonoBehaviour {
    public void Hide() {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<TextureConversion>().Hide();
        }
    }
}
