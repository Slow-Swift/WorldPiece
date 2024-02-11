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
    AudioSource[] keycodeSounds = new AudioSource[6];

    [SerializeField]
    AudioSource keycodePassSound, keycodeFailSound;

    [SerializeField]
    SpriteRenderer[] keycodeRenderers = new SpriteRenderer[keycodeLength];

    [SerializeField]
    SpriteRenderer loadedIndicator;

    // These could be replaced with images / sprites
    [SerializeField]
    Color unloadedColor, loadedColor;

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
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        // Update the keycode visuals
        for (int i = 0; i < keycodeRenderers.Length; i++)
        {
            Sprite keycodeSprite = i < keycode.Count ? keycodeSprites[keycode[i]] : null;
            keycodeRenderers[i].sprite = keycodeSprite;
        }

        // Update the loaded indicator light
        loadedIndicator.color = Loaded ? loadedColor : unloadedColor;
    }

    void HandleKeyPresses()
    {

        foreach (KeyCode kc in keycodeChars)
        {
            if (Input.GetKeyDown(kc) && (Loaded || keycode.Count == 0 || kc != keycodeChars[keycode[0]]))
            {
                keycodeFailSound.Play();
            }
        }

        if (Loaded) return;
        
        if (keycode.Count > 0 && Input.GetKeyDown(keycodeChars[keycode[0]])) {
            keycodeSounds[keycode[0]].Play();
            keycode.RemoveAt(0);

            if (keycode.Count == 0)
            {
                keycodePassSound.PlayDelayed(0.1f);
                Loaded = true;
                GenerateKeycode(keycodeLength);
            }
        }
    }

    /** Generate a new keycode of a given length
     */
    void GenerateKeycode(int length)
    {
        keycode.Clear();

        for (int i = 0; i < length; i++)
        {
            keycode.Add(Random.Range(0, keycodeChars.Length-1));
        }
    }

    public void Unload()
    {
        Loaded = false;
    }
}
