using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileDialog : MonoBehaviour {
    public static FileDialog instance;

    public Transform filePrefab;
    public Transform uiRoot;
    public Transform fileListContent;
    public InputField location;
    public Text selectedFilename;

    Action<string> OnFileSelected;

    string path;

    void Awake() {
        instance = this;
        uiRoot.gameObject.SetActive(true);
        Hide();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show(string path, Action<string> OnFileSelected) {
        this.OnFileSelected = OnFileSelected;
        Show(path);
    }

    public void Show(string path) {
        gameObject.SetActive(true);

        ClearFiles();

        try {
            this.path = path;
            location.text = path;
            var directories = Directory.GetDirectories(path);

            path = path + Path.DirectorySeparatorChar;

            foreach (string filename in directories) {
                AddEntry(Path.GetFileName(filename));
            }

            var files = Directory.GetFiles(path);

            foreach (string filename in files) {
                AddEntry(Path.GetFileName(filename));
            }
        }
        catch (Exception) {

        }
    }

    void AddEntry(string name) {
        var go = Instantiate<Transform>(filePrefab).gameObject;
        go.transform.SetParent(fileListContent);
        go.GetComponentInChildren<Text>().text = name;
        var button = go.GetComponent<Button>();
        button.onClick.AddListener(() => {
            OnSelectFile(name);
        });
    }

    void ClearFiles() {
        for (int i = 0; i < fileListContent.childCount; i++) {
            Destroy(fileListContent.GetChild(i).gameObject);
        }
    }

    void OnSelectFile(string file) {
        selectedFilename.text = file;
    }

    public void OnOKSelected() {
        string filenameStr = Path.Combine(path, selectedFilename.text);
        if (Directory.Exists(filenameStr)) {
            Show(filenameStr);
            return;
        }

        if (OnFileSelected != null) {
            OnFileSelected(filenameStr);
        }

        gameObject.SetActive(false);
    }

    public void OnLocationChanged(InputField input) {
        Show(input.text);
    }
}
