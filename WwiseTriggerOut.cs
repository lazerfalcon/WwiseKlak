//
// Wwise Klak Extensions
// Copyright (C) 2018 Shawn Laptiste
//
// Extensions to Klak - Utilities for creative coding with Unity (by Keijiro Takahashi)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

[UnityEngine.AddComponentMenu("Klak/Wiring/Wwise/Trigger Output")]
public class WwiseTriggerOut : Klak.Wiring.NodeBase
{
	#region Editable properties

	[UnityEngine.SerializeField]
	AK.Wwise.Trigger Trigger = new AK.Wwise.Trigger();

	[UnityEngine.SerializeField]
	UnityEngine.GameObject ReferenceGameObject = null;

	#endregion

	#region Node I/O

	[Klak.Wiring.Inlet]
	public void Bang()
	{
		Trigger.Post(ReferenceGameObject);
	}
	#endregion

	#region MonoBehaviour functions
	private void Start()
	{
		if (!ReferenceGameObject)
			ReferenceGameObject = transform.parent.gameObject;
	}

	#endregion
}
