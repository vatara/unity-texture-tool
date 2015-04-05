using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class TextureTypeUI : MonoBehaviour {
    public Image image;

    public virtual void OnOpenSelected() {
        FileDialog.instance.Show(Application.dataPath, OnFileSelected);
    }

    protected virtual void OnFileSelected(string filename) {
        StartCoroutine(LoadTexture(filename));
    }

    protected IEnumerator LoadTexture(string filename) {
        var url = "file://" + filename.Replace(@"\", "/");
        var www = new WWW(url);
        yield return www;
        var texture = www.texture;
        texture.anisoLevel = 16;
        texture.filterMode = FilterMode.Trilinear;
        SetTexture(texture);
    }

    public abstract void SetTexture(Texture2D texture);
}
