using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour {

	private _GameController _GameController;
	private SpriteRenderer spriteRenderer;
	public Sprite[] imagemObjeto;
	public bool open;

	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		
		spriteRenderer = GetComponent<SpriteRenderer>();

	}
	public void interacao(){
		open = !open;

		switch (open)
		{
			case true:
				spriteRenderer.sprite = imagemObjeto[1];

				if (_GameController == null)
				{
					_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
				}
				
				_GameController.teste += 1;
				break;

			case false:
				spriteRenderer.sprite = imagemObjeto[0];
				break;
		}
	}
}
