using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycodeManager : MonoBehaviour
{
    const int keycodeLength = 4;
    KeyCode[] keycodeChars = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D };

    [SerializeField]
    Sprite[] keycodeSprites = new Sprite[6];

    [SerializeField]
    SpriteRenderer[] keycodeRenderers = new SpriteRenderer[keycodeLength];

    public bool Loaded { get; private set; }

    List<int> keycode = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(keycodeSprites.Length == keycodeChars.Length, "Number of keycode spirtes does not match the number of keycode characters");
        GenerateKeycode(keycodeLength);
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyPresses();

        for (int i = 0; i < keycodeRenderers.Length; i++) {
            Sprite keycodeSprite = i < keycode.Count ? keycodeSprites[keycode[i]] : null;
            keycodeRenderers[i].sprite = keycodeSprite;
        }
    }

    void HandleKeyPresses()
    {
        if (Loaded) return;
        
        if (keycode.Count > 0 && Input.GetKeyDown(keycodeChars[keycode[0]])) {
            keycode.RemoveAt(0);

            if (keycode.Count == 0)
            {
                Loaded = true;
                GenerateKeycode(keycodeLength);
            }
        }
    }

    /** Generate a new keycode of a given length
     */
    void GenerateKeycode(int length)
    {
        Debug.Log("Generating keycode of length " + length);
        keycode.Clear();

        for (int i = 0; i < length; i++)
        {
            keycode.Add(Random.Range(0, keycodeChars.Length-1));
        }
    }
}
