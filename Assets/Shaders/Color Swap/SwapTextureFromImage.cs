using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapTextureFromImage : MonoBehaviour {

	Image image;
	Texture2D mColorSwapTex;
	Color[] mSpriteColors;

	public List<Color32> targetColors;
	public List<Color32> swapColors;

	void SwapFromLists (int index)
	{
		if (swapColors == null
		   || targetColors == null
		   || index >= targetColors.Count
		   || index >= swapColors.Count)
			return;
		SwapColor (targetColors[index].r, swapColors[index]);
	}

	void FillSwapTextureColors ()
	{
		for (int i = 0; i < targetColors.Count; ++i)
			SwapFromLists (i);
	}

	void Start () {
		image = GetComponent<Image> ();
		ScoreBarBehaviour b = GetComponent<ScoreBarBehaviour> ();

		InitColorSwapTex ();
		FillSwapTextureColors ();
		mColorSwapTex.Apply();
	}

	public void InitColorSwapTex()
	{
		Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
		colorSwapTex.filterMode = FilterMode.Point;

		for (int i = 0; i < colorSwapTex.width; ++i)
			colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

		colorSwapTex.Apply();


		image.material = new Material(Shader.Find("Unlit/Color Replacement"));
		image.material.SetTexture("_SwapTex", colorSwapTex);
		mSpriteColors = new Color[colorSwapTex.width];
		mColorSwapTex = colorSwapTex;
	}

	public void SwapColor(int index, Color color)
	{
		mSpriteColors[(int)index] = color;
		mColorSwapTex.SetPixel((int)index, 0, color);
	}

	public void SwapAllSpritesColorsTemporarily(Color color)
	{
		for (int i = 0; i < mColorSwapTex.width; ++i)
			mColorSwapTex.SetPixel(i, 0, color);
		mColorSwapTex.Apply();
	}
}
	