﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] float height;
    public SpeedType speedType = SpeedType.x;
    public float normalSpeed, middleSpeed, fastSpeed;
    public List<Train> trains;
    public bool isStarted;

    void Start()
    {
        foreach (Train item in FindObjectsOfType<Train>().ToList())
        {
            trains.Add(item);
        }
    }
    public void ResumeStartedTrain()
    {
        foreach (Train item in trains)
        {
            item.ResumeTrain();
        }
    }
    public void StopAllTrains()
    {
        foreach (Train item in trains)
        {
            item.StopTrain();
        }
    }
    public bool OnTrainReachedRail(Rail lastRail)
    {
        if(levelManager != null)
        {
            return levelManager.TrainReachedTarget(lastRail);
        }
        return false;
    }
    public void StartTrains()
    {
        if( !isStarted && trains.Count > 0 )
        {
            foreach (Train item in trains)
            {
                item.StartTrain();
            }
            isStarted = true;
        }
        else
        {
            ResumeStartedTrain();
        }
        
    }
    public void CreateTrain(GameObject choosenRail,GameObject trainPrefab, int _cost)
    {
        Rail r = choosenRail.GetComponent<Rail>();
        if( r != null && r.floorAdder == 0)
        {
            GameObject a = Instantiate(trainPrefab);
            RailConnectionPoint  startingPoint = r.GetInputConnectionPoints()[0];
            a.transform.position = new Vector3(startingPoint.point.x, startingPoint.point.y + height, startingPoint.point.z);
            a.transform.rotation = choosenRail.transform.rotation;

            Train t = a.transform.GetChild(0).GetComponent<Train>();
            t.rail = r;
            t.cost = _cost;
            t.startingRailId = t.rail.index;
            trains.Add(t);
        }
        else
        {
            Debug.Log("You should choose a rail");
        }
    }
    public void ChangeSpeed()
    {
        if(speedType == SpeedType.x) speedType = SpeedType.x2;   
        else if(speedType == SpeedType.x2) speedType = SpeedType.x3;
        else if(speedType == SpeedType.x3) speedType = SpeedType.x;
        foreach (Train item in trains)
        {
            item.SetSpeed();
        }   
    }
    public void RemoveTrain(Train t)
    {
        trains.Remove(t);
    }
    public List<Train> GetTrains()
    {
        return trains;
    }
}
