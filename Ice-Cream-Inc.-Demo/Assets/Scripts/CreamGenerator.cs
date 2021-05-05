using System.Collections.Generic;
using UnityEngine;

public class CreamGenerator : MonoBehaviour
{
    public static int currentLevel = 1;
    public static bool move = false;

    [SerializeField] GameObject cream,container;
    [SerializeField] Material[] creamType;

    TransformGenerator _transformGenerator;
    LevelManager _levelManager;
    ProgressBar _progressBar;

    List<Vector3> base0;
    List<GameObject> creams0;
    Vector3 machineTransform;

    float nextActionTime, period;
    int i, j = 0;
    bool levelFinishedflag = false;

    
    
    void Start()
    {
        _progressBar = FindObjectOfType<ProgressBar>();
        _levelManager = FindObjectOfType<LevelManager>();
        _transformGenerator = FindObjectOfType<TransformGenerator>();
        container = GameObject.FindGameObjectWithTag("CreamContainer");


        base0 = new List<Vector3>();
        creams0 = new List<GameObject>();

        nextActionTime = 0;
        period = 0.006f;
        base0 = _transformGenerator.GetTransforms();

        Debug.Log(base0.Count);

        machineTransform = new Vector3(transform.position.x,transform.position.y-1,transform.position.z);
        transform.parent.position = new Vector3(base0[0].x, 8.5f, base0[0].z);

        
    }

    void FixedUpdate()
    {
        if (UserInput.chocolateDown||UserInput.vanilliaDown)
        {
            InstantiateCream();
            MachineMove();
        }
        CreamDown();
    }
    void CreamDown()
    {
     
        for (int i = 0; i < creams0.Count; i++)
        {
            if (creams0[i].transform.position.y<8)
            {
                Quaternion rot = Quaternion.LookRotation(-base0[i]);
                creams0[i].transform.rotation = Quaternion.Lerp(creams0[i].transform.rotation, rot, Time.deltaTime * 1f);
            }
            creams0[i].transform.position = Vector3.MoveTowards(creams0[i].transform.position, base0[i], Time.deltaTime * 3f);
          
        }
        if (creams0.Count==base0.Count-1)
        {
            if (creams0[creams0.Count - 1].transform.position == base0[creams0.Count - 1] && levelFinishedflag == false)
            {
                Debug.Log("LevelFinished");
                _levelManager.CalculateMatch();
                levelFinishedflag = true;
            }
        }
    }
    void InstantiateCream(){
        if (Time.time > nextActionTime)
        {
            nextActionTime += period    ;
            if (i < base0.Count - 1)
            {
                machineTransform = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                creams0.Add(Instantiate(cream, machineTransform, Quaternion.identity,container.transform));

                creams0[i].transform.eulerAngles = new Vector3(0, 0, 90);
                creams0[i].name = i.ToString();

                _progressBar.IncrementProgress(creams0.Count );
               
                if (UserInput.chocolateDown)
                    creams0[i].GetComponentInChildren<MeshRenderer>().material = creamType[0];
                else
                    creams0[i].GetComponentInChildren<MeshRenderer>().material = creamType[1];

                i++;
            }
        }
    }
    void MachineMove()
    {

        if (j < base0.Count)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position,
                new Vector3(base0[j].x, 8.5f, base0[j].z), Time.deltaTime * (12f-base0[i].y*2.5f));

            if (transform.parent.position.x == base0[j].x)
                j++;
        }

    }

    public List<Material> getAnswer()
    {
        List<Material> userAnswer = new List<Material>();

        for (int i = 0; i < creams0.Count; i++)
            userAnswer.Add(creams0[i].GetComponentInChildren<MeshRenderer>().material);

        return userAnswer;
    }
    public void DeleteCreams()
    {
        creams0.Clear();

        foreach (Transform child in container.transform)
            Destroy(child.gameObject);

        i = 0;
        j = 0;

        transform.parent.position = new Vector3(base0[0].x, 8.5f, base0[0].z);
    }
    public void NextLevel()
    {  
        levelFinishedflag = false;
        currentLevel++;
        if (currentLevel == 3)
            currentLevel = 1;
    }
}
