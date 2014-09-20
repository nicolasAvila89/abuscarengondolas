﻿using UnityEngine;
using System.Collections;
using System;

public class Service : MonoBehaviour
{
	private const int MAX_DEFAULT_RETRIES = 3;
	private const int DEFAULT_TIMEOUT_SECONDS = 10;
	private string _URL;
	private string _requestId;
	private int _secondsTimeout = DEFAULT_TIMEOUT_SECONDS;
	private int _retryIntent;
	private int _maxRetries = MAX_DEFAULT_RETRIES;
	private Action<SharedObject, Exception> _action;
	private SharedObject _inputData;
	private WWW _WWW;
	private long _startTime;

	public string RequestId {
		get { return _requestId; }
	}

	internal Service ()
	{
		_requestId = RandomUtils.RandomAlphaNumericString (5);
		DontDestroyOnLoad (this);
	}

	internal Service WithURL (string URL)
	{
		_URL = URL;
		return this;
	}

	public Service WithSecondsTimeout (int secondsTimeout)
	{
		_secondsTimeout = secondsTimeout;
		return this;
	}
	
	public Service WithMaxRetries (int maxRetries)
	{
		_maxRetries = maxRetries;
		return this;
	}

	public Service WithInputData (SharedObject inputData)
	{
		_inputData = inputData;
		return this;
	}

	public void Call (Action<SharedObject, Exception> action)
	{
		_startTime = TimeUtils.NowTicks;
		_action = action;
		StartCoroutine ("CallImpl");
	}

	// For timeout!
	private void Update ()
	{
		if (_startTime > 0) {
			if (TimeUtils.TimePassed (_startTime).Seconds >= _secondsTimeout) {
				_WWW.Dispose ();
				_WWW = null;
				_startTime = 0;
				ThreatError ("Service with URL [" + _URL + "] failed with reason timeouted ([" + _secondsTimeout + "] seconds)");
			}
		}
	}

	private IEnumerator CallImpl ()
	{
		if (_inputData == null)
			_WWW = new WWW (_URL);
		else
			_WWW = new WWW (_URL, _inputData.Serialize ());
		yield return _WWW;

		if (_WWW == null || !_WWW.isDone) {
			yield break;
		}

		bool remove = true; 	
		if (_WWW.error != null) {
			remove = !ThreatError ("Service with URL [" + _URL + "] failed with reason [" + _WWW.error + "]");
		} else {
			_action (SharedObject.Deserialize (_WWW.bytes), null);
		}

		if (remove) {
			_WWW.Dispose ();
			_WWW = null;
			Destroy (this);
		}
	}

	private bool ThreatError (string message)
	{
		Debug.LogWarning (message);
		if (_maxRetries > _retryIntent++) {
			Debug.LogWarning ("Retrying service with URL [" + _URL + "]. Intent [" + _retryIntent + "] of [" + _maxRetries + "]");
			Call (_action);
			return true;
		} 

		_action (null, new Exception (message));
		return false;
	}
}