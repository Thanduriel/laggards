using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CurveGenerator : MonoBehaviour
{
	public TileBase GroundTileType;
	public int SizeX;
	public int SizeY;
	public int Seed;
	public float Frequency;

	// Use this for initialization
	void Start()
	{
		Tilemap map = GetComponent<Tilemap>();

		System.Random rng = Seed == 0 ? new System.Random() : new System.Random(0xffaabb);

		// +1 so that the higher bound is defined for the last segment
		int numValues = (int)(Math.Ceiling(SizeX * Frequency)) + 1;
		float[] values = new float[numValues];
		for (int i = 0; i < values.Length; ++i)
			values[i] = (float)rng.NextDouble() * (SizeY + 0.5f); // the largest size should actually be reachable

		for (int i = 0; i < SizeX; ++i){
			float cur = Frequency * i;
			int begin = (int)Math.Floor(cur);
			int end = (int)Math.Ceiling(cur);
			float test = Interpolate(values[begin], values[end], cur - begin);
			map.SetTile(new Vector3Int(i, (int)Math.Round(Interpolate(values[begin],values[end], cur - begin)),0), GroundTileType);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	private static int Sin(int x)
	{
		return (int)(Mathf.Cos(x / 2) * 1.2f);
	}

	private float BlendCubic(float _x)
	{
		return 3f* _x*_x - 2f* _x * _x* _x;
	}

	private float Interpolate(float a, float b, float x)
	{
		return (1f - BlendCubic(x)) * a + BlendCubic(x) * b;
	}

}
