using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LukeTerrain : MonoBehaviour
{
	public float baseDepth = 2f;
	public float depth;
	public float volume;
	public int length = 32;
	public int width = 32;
	public float frequency = 5f;
	public float amplitude = 1f;
	public float xOffset = 1f;
	public float yOffset = 1f;
	public Vector2 offsetSpeeds = new(5,0);
	public float angle = 0f;
	public float changeRotationDelay = 2f;

	TerrainData GenerateTerrain(TerrainData terrainData)
	{
		terrainData.heightmapResolution = width + 1;
		
		terrainData.size = new Vector3(width, depth, length);
		
		terrainData.SetHeights(0,0,GenerateHeights());

		return terrainData;
	}

	float[,] GenerateHeights()
	{
		float[,] heights = new float[width, length];
		for (int i=0; i<width; i++)
		{
			for (int j=0; j < length; j++)
			{
				heights[i, j] = CalculateHeight(i, j);	
			}
		}

		return heights;
	}

	float CalculateHeight(int x, int y)
	{
		float xCoord = (float) x / width*frequency + xOffset;
		float yCoord = (float) y / length*frequency + yOffset;

		return amplitude * Mathf.PerlinNoise( xCoord,  yCoord);
	}

	public void VolumeEffect(short value)
	{
		volume = value/3f;
		StartCoroutine(TweenDepth());
	}
	
	private IEnumerator TweenDepth()
	{
		DOTween.To(ChangeDepth, depth, volume, 0.5f);
		yield return new WaitForSeconds(0.3f);
		DOTween.To(ChangeDepth, depth, baseDepth, 0.5f);
	}

	void ChangeDepth(float value)
	{
		depth = value;
	}

	void RotateAngle(float degrees)
	{
		angle = degrees;
		offsetSpeeds = RotateVector2(offsetSpeeds, angle);
	}
	
	private IEnumerator RandomRotateOffsetSpeeds()
	{
		float target;
		
		target = angle + Random.Range(-45, 45);

		DOTween.To(RotateAngle, angle, target, changeRotationDelay);

		yield return new WaitForSeconds(6f);
		StartCoroutine(RandomRotateOffsetSpeeds());
	}

	public Vector2 RotateVector2(Vector2 vector, float degree)
	{
		return Quaternion.Euler(0,0,degree) * vector;
	}
	
	void Start()
	{
		depth = baseDepth;	
		StartCoroutine(RandomRotateOffsetSpeeds());
	}
	
	void Update()
	{
		offsetSpeeds = RotateVector2(offsetSpeeds, angle-Mathf.Atan2(offsetSpeeds[1],offsetSpeeds[0]));
		xOffset += Time.deltaTime * offsetSpeeds[0];
		yOffset += Time.deltaTime * offsetSpeeds[1];
		Terrain terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain(terrain.terrainData);
	}
}