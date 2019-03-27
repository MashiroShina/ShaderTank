using System.Collections;
using System.Collections.Generic;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.UI;

public class SaveTank : MonoBehaviour {
	public Texture2D r1;
	public Texture2D r2;
	public RawImage image;
	public Shader sb;
	public Material mt;
	public RenderTexture rt;
	

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			Shader.DisableKeyword("_HAVECOLOR_ON");
			Shader.DisableKeyword("_HAVECOLOR_OFF");
			Shader.DisableKeyword("_CLIPPING_NULL");
			Shader.DisableKeyword("_CLIPPING_OFF");
			Shader.EnableKeyword("_CLIPPING_ON");
			Shader.EnableKeyword("_HAVECOLOR_NULL");
		}
		if (Input.GetKeyDown(KeyCode.O))
		{
			Shader.DisableKeyword("_CLIPPING_OFF");
			Shader.DisableKeyword("_CLIPPING_ON");
			Shader.DisableKeyword("_HAVECOLOR_OFF");
			Shader.DisableKeyword("_HAVECOLOR_NULL");
			Shader.EnableKeyword("_CLIPPING_NULL");
			Shader.EnableKeyword("_HAVECOLOR_ON");
		}
		if (Input.GetKeyDown(KeyCode.I))
		{
			Shader.DisableKeyword("_CLIPPING_OFF");
			Shader.DisableKeyword("_CLIPPING_ON");
			Shader.EnableKeyword("_HAVECOLOR_OFF");
			Shader.DisableKeyword("_HAVECOLOR_NULL");
			Shader.EnableKeyword("_CLIPPING_NULL");
			Shader.DisableKeyword("_HAVECOLOR_ON");
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			Shader.EnableKeyword("_CLIPPING_OFF");
			Shader.DisableKeyword("_CLIPPING_ON");
			Shader.DisableKeyword("_HAVECOLOR_OFF");
			Shader.DisableKeyword("_HAVECOLOR_NULL");
			Shader.DisableKeyword("_CLIPPING_NULL");
			Shader.DisableKeyword("_HAVECOLOR_ON");
		}
		if (Input.GetKey(KeyCode.K))
		{
			DumpRenderTexture(rt,Application.dataPath+"Tank.png");
			Debug.Log("OK");
		}
		
	}

	public void ShowCam(bool Show)
	{
		Camera.main.backgroundColor = Show ? Color.black : Color.white;
	}
	public void OnlyColor(bool not)
	{
		if (!not)
		{
			Shader.EnableKeyword("_CLIPPING_OFF");
			Shader.DisableKeyword("_CLIPPING_ON");
			Shader.DisableKeyword("_HAVECOLOR_OFF");
			Shader.DisableKeyword("_HAVECOLOR_NULL");
			Shader.DisableKeyword("_CLIPPING_NULL");
			Shader.DisableKeyword("_HAVECOLOR_ON");
		}
		else
		{
			Shader.DisableKeyword("_CLIPPING_OFF");
			Shader.DisableKeyword("_CLIPPING_ON");
			Shader.EnableKeyword("_HAVECOLOR_OFF");
			Shader.DisableKeyword("_HAVECOLOR_NULL");
			Shader.EnableKeyword("_CLIPPING_NULL");
			Shader.DisableKeyword("_HAVECOLOR_ON");
		}
	}

	public void savePngs()
	{
		DumpRenderTexture(rt,Application.dataPath+"Tank.png");
	}
	public static void DumpRenderTexture(RenderTexture rt, string pngOutPath)
	{
		var oldRT = RenderTexture.active;
 
		var tex = new Texture2D(rt.width, rt.height);
		RenderTexture.active = rt;
		tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
		tex.Apply();
		File.WriteAllBytes(pngOutPath, tex.EncodeToPNG());
		RenderTexture.active = oldRT;
	}

	public void SetTexture(string tex) {
		var paths = StandaloneFileBrowser.OpenFilePanel("打开用户数据文件", "%HOMEDRIVE/Desktop%", "", false);
		if (paths.Length > 0) {
			StartCoroutine(OutputRoutine(tex,new System.Uri(paths[0]).AbsoluteUri));
		}
	}

	private IEnumerator OutputRoutine(string tex,string url)
	{
		var loader = new WWW(url);
		yield return loader;
		Texture2D t= loader.texture;
		mt.SetTexture(tex,t);
	}

}
