using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
	public Text displayText;
	float deltaTime = 0.0f;

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

		if(displayText != null)
		{
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			displayText.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		}
	}
}