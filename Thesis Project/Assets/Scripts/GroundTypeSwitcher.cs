using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTypeSwitcher : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private PanelSwitch _panelSwitch;

    public Color grassColor;
    public Color normalColor;
    public Color iceColor;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _panelSwitch = FindObjectOfType<PanelSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_panelSwitch.currentPosition)
        {
            case 0:
                _spriteRenderer.color = grassColor;
                break;
            case 1:
                _spriteRenderer.color = normalColor;
                break;
            case 2:
                _spriteRenderer.color = iceColor;
                break;
        }
    }
}
