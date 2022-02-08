using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridgeneration : MonoBehaviour
{

    public GameObject prefab;

    public Color BlankColour, Visible;
    GameObject Maincube;
    GameObject child; 

    [Range(1,8)]
    public int SizeY;
    [Range(1, 8)]
    public int SizeX;
    [Range(1, 8)]
    public int SizeZ;

    // Start is called before the first frame update
    void Start()
    {

        StartGen();

//        Maincube = gameObject;
  //      child = Maincube.transform.GetChild(0).gameObject;
    }
    public void StartGen()
    {

        Delete();

        for (int x = 0; x< SizeX; x++)
        {
            for (int y= 0; y < SizeY; y++)
            {
                for(int z =0; z < SizeZ; z++)
                {
                    GameObject newGridPiece = Instantiate(prefab, transform);
                    Vector3 newPosition = new Vector3(x, y, z);
                    newGridPiece.transform.localPosition = newPosition;


                    newGridPiece.TryGetComponent(out Renderer rend);


                    float interp = (float)transform.childCount / (SizeX + SizeZ + SizeY);
                    Debug.Log(interp);
                    
                    if(Application.isEditor)
                    {
                        rend.sharedMaterial.color = Color.Lerp(BlankColour, Visible, interp);

                    }
                    else
                    {
                        rend.material.color = Color.Lerp(BlankColour, Visible, interp);
                    }

                }
            }
        }
    }

    public void Delete()
    {
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
            foreach (Transform i in transform) Destroy(i.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
