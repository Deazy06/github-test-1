using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour

{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    public System.Action killed;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;



    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        InvokeRepeating(nameof(AnimateSprite), _animationFrame, animationTime);
    }
    
   
    private void AnimateSprite()
    {
        _animationFrame++;

        if (_animationFrame >= animationSprites.Length)
        {
            _animationFrame = 0;
        }
        _spriteRenderer.sprite = animationSprites[_animationFrame];

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("laser"))
        {
            killed();
            gameObject.SetActive(false);
        }
    }
}
