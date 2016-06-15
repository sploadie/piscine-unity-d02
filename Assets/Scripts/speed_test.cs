using UnityEngine;
using System.Collections;

public class speed_test : MonoBehaviour {
	
	readonly int iterations = 10000000;
	Vector3 testVector = new Vector3(3f, 14f, 42f);
	
	void Start()
	{
		Debug.Log(string.Format("TestConvertByOperation {0} instances: {1}s", iterations, TestConvertByOperation()));
		Debug.Log(string.Format("TestConvertByCasting {0} instances: {1}s", iterations, TestConvertByCasting()));
		Debug.Log(string.Format("TestConvertByInitializing {0} instances: {1}s", iterations, TestConvertByInitializing()));
	}
	
	float TestConvertByOperation()
	{
		var timeStart = Time.realtimeSinceStartup;
		
		for (int i = 0; i < iterations; i++)
		{
			Vector2 v2 = testVector;
		}
		
		return Time.realtimeSinceStartup - timeStart;
	}
	
	float TestConvertByCasting()
	{
		var timeStart = Time.realtimeSinceStartup;
		
		for (int i = 0; i < iterations; i++)
		{
			Vector2 v2 = (Vector2)testVector;
		}
		
		return Time.realtimeSinceStartup - timeStart;
	}
	
	float TestConvertByInitializing()
	{
		var timeStart = Time.realtimeSinceStartup;
		
		for (int i = 0; i < iterations; i++)
		{
			Vector2 v2 = new Vector2(testVector.x, testVector.y);
		}
		
		return Time.realtimeSinceStartup - timeStart;
	}
	
}
