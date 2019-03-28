using System.Collections;
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
    public Material mt2;
	public RenderTexture rt;
    private void Awake()
    {
        //mt.DisableKeyword("_CLIPPING_OFF");
        //mt.DisableKeyword("_CLIPPING_ON");
        //mt.DisableKeyword("_HAVECOLOR_NULL");
        //mt.DisableKeyword("_HAVECOLOR_OFF");
        //mt.DisableKeyword("_HAVECOLOR_ON");
        //mt.DisableKeyword("_CLIPPING_NULL");
    }

    private void Update()
	{
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    //mt.DisableKeyword("_HAVECOLOR_ON");
        //    //mt.DisableKeyword("_HAVECOLOR_OFF");
        //    //mt.DisableKeyword("_CLIPPING_NULL");
        //    //mt.DisableKeyword("_CLIPPING_OFF");
        //    //mt.EnableKeyword("_CLIPPING_ON");
        //    //mt.EnableKeyword("_HAVECOLOR_NULL");
        
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //          mt.DisableKeyword("_CLIPPING_OFF");
        //          mt.DisableKeyword("_CLIPPING_ON");
        //          mt.DisableKeyword("_HAVECOLOR_OFF");
        //          mt.DisableKeyword("_HAVECOLOR_NULL");
        //          mt.EnableKeyword("_CLIPPING_NULL");
        //          mt.EnableKeyword("_HAVECOLOR_ON");
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //          mt.DisableKeyword("_CLIPPING_OFF");
        //          mt.DisableKeyword("_CLIPPING_ON");
        //          mt.EnableKeyword("_HAVECOLOR_OFF");
        //          mt.DisableKeyword("_HAVECOLOR_NULL");
        //          mt.EnableKeyword("_CLIPPING_NULL");
        //          mt.DisableKeyword("_HAVECOLOR_ON");
        //}
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //          mt.EnableKeyword("_CLIPPING_OFF");
        //          mt.DisableKeyword("_CLIPPING_ON");
        //          mt.DisableKeyword("_HAVECOLOR_OFF");
        //          mt.DisableKeyword("_HAVECOLOR_NULL");
        //          mt.DisableKeyword("_CLIPPING_NULL");
        //          mt.DisableKeyword("_HAVECOLOR_ON");
        //}
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
		//if (!not)
		//{
  //          mt.DisableKeyword("_CLIPPING_OFF");
  //          mt.DisableKeyword("_CLIPPING_ON");
  //          mt.DisableKeyword("_HAVECOLOR_NULL");
  //          mt.DisableKeyword("_HAVECOLOR_OFF");
  //          mt.EnableKeyword("_HAVECOLOR_ON");
  //          mt.EnableKeyword("_CLIPPING_NULL");
  //      }
		//else
		//{
  //          mt.DisableKeyword("_CLIPPING_OFF");
  //          mt.DisableKeyword("_CLIPPING_ON");
  //          mt.DisableKeyword("_HAVECOLOR_NULL");
  //          mt.DisableKeyword("_HAVECOLOR_ON");
  //          mt.EnableKeyword("_HAVECOLOR_OFF");
  //          mt.EnableKeyword("_CLIPPING_NULL");
  //      }
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
        var path = StandaloneFileBrowser.SaveFilePanel("Title", "", "sample", "png");
        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllBytes(path, tex.EncodeToPNG());
        }
       // File.WriteAllBytes(pngOutPath, tex.EncodeToPNG());
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
        mt2.SetTexture(tex, t);
    }

}
