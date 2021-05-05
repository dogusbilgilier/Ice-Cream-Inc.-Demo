using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformGenerator : MonoBehaviour
{
    float angle,radians,result;
    [SerializeField] GameObject go;
    float x, y,z;
    public List<Vector3> transformList0,transformList1, transformList2,transformList3, transformList4, transformList5, transformList6;
    int index=0;
    public static int[] baseTransformCount = new int[7];
    private void Start()
    {
        transformList0 = new List<Vector3>();
        transformList1 = new List<Vector3>();
        transformList2 = new List<Vector3>();
        transformList3 = new List<Vector3>();
        transformList4 = new List<Vector3>();
        transformList5 = new List<Vector3>();
        transformList6 = new List<Vector3>();

        

        y = transform.position.y;
        angle =0;
        Generate(y,transformList0,1f,2);
        Generate(y + 0.2f, transformList1, 0.90f, 3);
        Generate(y + 0.4f, transformList2, 0.8f, 4);
        Generate(y + 0.6f, transformList3, 0.6f, 5);
        Generate(y + 0.8f, transformList4, 0.4f, 8);
        Generate(y + 1f, transformList5, 0.2f, 12);
        Generate(y + 1.2f, transformList6, 0.05f, 15);

        baseTransformCount[0] = transformList0.Count;
        baseTransformCount[1] = transformList1.Count;
        baseTransformCount[2] = transformList2.Count;
        baseTransformCount[3] = transformList3.Count;
        baseTransformCount[4] = transformList4.Count;
        baseTransformCount[5] = transformList5.Count;
        baseTransformCount[6] = transformList6.Count;

    }
    public List<Vector3> GetTransforms()
    {
        transformList0.AddRange(transformList1);
        transformList0.AddRange(transformList2);
        transformList0.AddRange(transformList3);
        transformList0.AddRange(transformList4);
        transformList0.AddRange(transformList5);
        transformList0.AddRange(transformList6);
        return transformList0;
    }

    void Generate( float y,List<Vector3> transformlist,float radius,int counter)
    {
        
        for (int i = 0; i < 360; i += counter)
        {
            angle = i * Mathf.PI / 180;
            x = radius * Mathf.Cos(angle);
            z = radius * Mathf.Sin(angle);

            GameObject transforms = Instantiate(go, new Vector3(x, y, z), Quaternion.LookRotation(transform.position), transform);
            
            transformlist.Add(transforms.transform.position);
            index++;
        }
    }

}
