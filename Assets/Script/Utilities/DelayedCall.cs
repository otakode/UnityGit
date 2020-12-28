using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class DelayedCall
{
	public static void Invoke(this MonoBehaviour monoBehaviour, UnityAction action, float delay)
	{
		monoBehaviour.StartCoroutine(DelayedCallCoroutine(action, delay));
	}

	public static IEnumerator DelayedCallCoroutine(UnityAction action, float delay)
	{
		yield return new WaitForSeconds(delay);
		action();
	}
}
