using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CurveGenerator : MonoBehaviour
{
	public TileBase GroundTileType;
	public Func<int, int> Curve;
	public int SizeX;
	public int SizeY;

	// Use this for initialization
	void Start()
	{
		Tilemap map = GetComponent<Tilemap>();

		Curve = Sin;

		for (int i = -SizeX; i < SizeX; ++i)
			map.SetTile(new Vector3Int(i, Mathf.Clamp(Curve(i), -SizeY, SizeY), 0), GroundTileType);
	}

	// Update is called once per frame
	void Update()
	{

	}

	private static int Sin(int x)
	{
		return (int)(Mathf.Cos(x / 2) * 1.2f);
	}
}
