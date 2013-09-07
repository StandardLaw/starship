﻿using UnityEngine;
using System.Collections;

public class GlobeFetcher : MonoBehaviour {
	
	public string url = "http://dev.aasen.in:1337/github-globe/";
    
	IEnumerator Start() {
        WWW www = new WWW(url);
        yield return www;
        
		JSONObject j = new JSONObject(www.text);
	
		foreach (var jsonObject in j.list) {
			float r = gameObject.transform.localScale.x;
			
			float latitude = float.Parse(jsonObject.GetField("coordinates").GetField("latitude").ToString()) * ((float) System.Math.PI / 180f);
			float longitude = float.Parse(jsonObject.GetField("coordinates").GetField("longitude").ToString()) * ((float) System.Math.PI / 180f);			
			float magnitude = float.Parse(jsonObject.GetField("magnitude").ToString());
			
			float x = (float) -(r * System.Math.Cos(latitude) * System.Math.Sin(longitude));
			float y = (float) (r * System.Math.Sin(latitude));
			float z = (float) (r * System.Math.Cos(latitude) * System.Math.Cos(longitude));
			
			GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
        	
			bar.transform.position = gameObject.transform.position + (new Vector3(x, y, z) * 0.5f);
			bar.transform.localScale = new Vector3(0.05f, 0.05f, magnitude * 10f);
			bar.transform.LookAt(gameObject.transform);
			
		}
	}
}
