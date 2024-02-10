using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycodeManager : MonoBehaviour
{
    const int keycodeLength = 4;
    KeyCode[] keycodeChars = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D };

    [SerializeField]
    GameObject[] keycodeVisualizations = new GameObject[keycodeLength];

    public bool Loaded { get; private set; }

    List<KeyCode> keycode = new List<KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateKeycode(keycodeLength);
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyPresses();
    }

    void HandleKeyPresses()
    {
        if (Loaded) return;

        KeyCode nextKey = keycode[0];
        if (Input.GetKeyDown(nextKey)) {
            keycode.RemoveAt(0);
        }
    }

    /** Generate a new keycode of a given length
     */
    void GenerateKeycode(int length)
    {
        keycode.Clear();

        for (int i = 0; i < length; i++)
        {
            keycode.Add(keycodeChars[Random.Range(0, keycodeChars.Length)]);
        }
    }
}
