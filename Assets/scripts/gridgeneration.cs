using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridgeneration : MonoBehaviour
{

    public GameObject prefab;


    //GameObject Maincube;
    //GameObject child; 

    //Slider values for Grid Size coordinates
    [Range(1, 4)]
    public int SizeY;
    [Range(1, 12)]
    public int SizeX;
    [Range(1, 12)]
    public int SizeZ;

    MaterialPropertyBlock mpb;

    static readonly int shCubeColor = Shader.PropertyToID("_Color");


    public CubeLayer[] terrainLayers;



    public MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null)
                mpb = new MaterialPropertyBlock();
            return mpb;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        StartGen();

        //      Maincube = gameObject;
        //      child = Maincube.transform.GetChild(0).gameObject;
    }
    public void StartGen()
    {

        Delete();


        int CubeCount = 1;
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {

                for (int z = 0; z < SizeZ; z++)
                {
                    GameObject newGridPiece = Instantiate(prefab, transform);
                    Vector3 newPosition = new Vector3(x, y, z);
                    newGridPiece.transform.localPosition = newPosition;

                    Color ncol = GetDepthColor(y); // new Color((float)x / SizeX, (float)y / SizeY, (float)z / SizeZ);
                    //newGridPiece.TryGetComponent(out MeshRenderer rend);
                    MeshRenderer mesh = newGridPiece.GetComponent<MeshRenderer>();//  newGridPiece.GetComponent<MeshRenderer>().material.color = ncol;

                    ApplyColour(mesh, ncol);

                    
                    

                    CubeCount++;

                    //if (Application.isEditor)
                    //{


                    //     ApplyColour(CubeCount, rend);

                    //}
                    //else
                    //{
                    //     ApplyColour(CubeCount, rend);
                    //}

                }
            }
        }
    }

    private Color GetDepthColor(int y)
    {
        foreach (CubeLayer i in terrainLayers)
        {
            if (i.LayerDepth == y)
            {
                return i.layerColor;
            }
        }
        return new Color(0.5f, (float)y / SizeY, 0.5f);

    }

    public void Delete()
    {
        //in editor deletes the child objects of the gameobject to clear the grid (using DestroyImmediate)
        if (Application.isEditor)
        {

            List<GameObject> children = new List<GameObject>();
            foreach (Transform i in transform)
            {
                children.Add(i.gameObject);
            }

            GameObject[] childrenArray = children.ToArray();

            foreach (GameObject i in childrenArray) DestroyImmediate(i.gameObject);

        }
        else
        {
            //does the same but for during runtime (using Destroy)
            foreach (Transform i in transform) Destroy(i.gameObject);
        }
    }

  
    public void CubePen()
    {
        //to add later
        //material functionality
        //double click? / right click? to change cube material
    }

    void ApplyColour(MeshRenderer meshRenderer, Color color)
    {
        Mpb.SetColor(shCubeColor, color);
        meshRenderer.SetPropertyBlock(Mpb);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
