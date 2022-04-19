using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LukeTerrain : MonoBehaviour
{
	public float baseDepth = 2f;
	public float depth;
	public float volume;
	public int length = 64;
	public int width = 64; 
	public float frequency = 3f;
	public float amplitude = 1f;
	public float zOffset;
	public float xOffset;
	public float zOffsetSpeed = 3f;

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
		float xCoord = (float) x / width*frequency + zOffset;
		float yCoord = (float) y / length*frequency + xOffset;

		return amplitude * Mathf.PerlinNoise( xCoord,  yCoord);
	}

	public void VolumeEffect(short value)
	{
		volume = value/2.5f;
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

	public Vector2 RotateVector2(Vector2 vector, float degree)
	{
		return Quaternion.Euler(0,0,degree) * vector;
	}
	
	void Start()
	{
		depth = baseDepth;
		xOffset = Random.Range(1f, 1000f);
		zOffset = Random.Range(1f, 1000f);
	}
	
	void Update()
	{
		zOffset += Time.deltaTime * zOffsetSpeed;
		Terrain terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain(terrain.terrainData);
	}
}