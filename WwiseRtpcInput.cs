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

[UnityEngine.AddComponentMenu("Klak/Wiring/Wwise/Rtpc Input")]
public class WwiseRtpcInput : Klak.Wiring.NodeBase
{
	#region Editable properties

	[UnityEngine.SerializeField]
	AK.Wwise.RTPC RTPC = new AK.Wwise.RTPC();

	[UnityEngine.SerializeField]
	bool UseGlobalRTPC = true;

	[UnityEngine.SerializeField]
	UnityEngine.GameObject ReferenceGameObject = null;

	[UnityEngine.SerializeField]
	Klak.Math.FloatInterpolator.Config _interpolator;
	#endregion

	#region Node I/O

	[UnityEngine.SerializeField, Klak.Wiring.Outlet]
	Klak.Wiring.NodeBase.FloatEvent _valueEvent = new Klak.Wiring.NodeBase.FloatEvent();

	#endregion

	#region MonoBehaviour functions

	Klak.Math.FloatInterpolator _axisValue;

	private void Start()
	{
		_axisValue = new Klak.Math.FloatInterpolator(0, _interpolator);

		if (!ReferenceGameObject)
			ReferenceGameObject = transform.parent.gameObject;
	}

	void Update()
	{
		if (!RTPC.IsValid())
			return;

		var akQueryRTPCValue = UseGlobalRTPC || ReferenceGameObject == null ? AkQueryRTPCValue.RTPCValue_Global : AkQueryRTPCValue.RTPCValue_GameObject;
		int queryValue = (int)akQueryRTPCValue;

		float value = 0;
		if (AKRESULT.AK_Success == AkSoundEngine.GetRTPCValue((uint)RTPC.ID, ReferenceGameObject, AkSoundEngine.AK_INVALID_PLAYING_ID, out value, ref queryValue))
			_axisValue.targetValue = value;

		_valueEvent.Invoke(_axisValue.Step());
	}

	#endregion
}
