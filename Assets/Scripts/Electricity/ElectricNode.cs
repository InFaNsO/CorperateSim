using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ElectricNode : MonoBehaviour
{
    public enum AttachedToBody
    {
        Producer,
        Pole,
        Consumer
    }

    [SerializeField] AttachedToBody body;

    public ElectricNode other = null;

    [SerializeField] public ElectricConsumer consumer;
    [SerializeField] public ElectricPole pole;
    [SerializeField] public ElectricProducer producer;

    List<ElectricNode> closedList = new List<ElectricNode>();
    List<ElectricNode> openList = new List<ElectricNode>();

    public bool isOpened = false;
    public bool isClosed = false;

    void Start()
    { 
        
    }

    void Update()
    {
        switch (body)
        {
            case AttachedToBody.Producer:
                break;
            case AttachedToBody.Pole:
                break;
            case AttachedToBody.Consumer:
                break;
            default:
                break;
        }
    }

    void Checker(ElectricNode other)
    {
        if(!other.isOpened && !other.isClosed)
        {
            openList.Add(other);
            other.isOpened = true;
        }
    }

    void EchoReduction(int power)
    {
        ResetAll();
        openList.Add(this);
        isOpened = true;

        while(openList.Count > 0)
        {
            var node = openList[0];
            openList.RemoveAt(0);

            if (node.body == AttachedToBody.Pole)
            {
                node.pole.AvailableElectricity += power;
                for (int i = 0; i < pole.myNodes.Count; ++i)
                {
                    Checker(pole.myNodes[i]);
                }
            }

            closedList.Add(node);
            node.isClosed = true;
        }
    }

    void EchoShutDown(int power)
    {
        ResetAll();
        openList.Add(this);
        isOpened = true;

        while (openList.Count > 0)
        {
            var node = openList[0];
            openList.RemoveAt(0);

            if(node.body == AttachedToBody.Consumer)
            {
                Checker(node.other);
                node.consumer.ShutDown();
            }
            else if (node.body == AttachedToBody.Pole)
            {
                node.pole.AvailableElectricity += power;
                for (int i = 0; i < pole.myNodes.Count; ++i)
                {
                    Checker(pole.myNodes[i]);
                }
            }
            else
            {
                node.producer.ShutDouwn();
            }

            closedList.Add(node);
            node.isClosed = true;
        }
    }

    void EchoReset()
    {
        List<ElectricNode> producersList = new List<ElectricNode>();

        ResetAll();
        openList.Add(this);
        isOpened = true;

        while (openList.Count > 0)
        {
            var node = openList[0];
            openList.RemoveAt(0);

            if (node.body == AttachedToBody.Consumer)
            {
                Checker(node.other);
                node.consumer.ShutDown();
            }
            else if (node.body == AttachedToBody.Pole)
            {
                node.pole.AvailableElectricity = 0;
                for (int i = 0; i < pole.myNodes.Count; ++i)
                {
                    Checker(pole.myNodes[i]);
                }
            }
            else
            {
                producersList.Add(node);
            }

            closedList.Add(node);
            node.isClosed = true;
        }

        for (int i = 0; i < producersList.Count; ++i)
            producer.AddPower();

    }

    public void ResetThis()
    {
        isOpened = false;
        isClosed = false;
    }
    public void ResetAll()
    {
        for(int i = 0; i < closedList.Count; ++i)
            closedList[i].ResetThis();

        closedList.Clear();
        openList.Clear();
    }

    public bool GetPower(int power)
    {
        switch (body)
        {
            case AttachedToBody.Producer:
                //check energy
                return producer.PowerProduction > Mathf.Abs(power);
            case AttachedToBody.Pole:
                //Echo to other poles
                if (pole.AvailableElectricity - power > 0)
                {
                    //echo reduction
                    EchoReduction(power);
                    return true;
                }
                else
                {
                    //echo failiure
                    EchoShutDown(power);
                    return false;
                }
            default:
                return false;
        }
    }

    public void AddPower(int power)
    {
        if(body == AttachedToBody.Pole)
            EchoReduction(power);
    }

    public bool HavePower()
    {
        if (body == AttachedToBody.Producer)
        {
            return producer.PowerProduction > 0;
        }
        else if (body == AttachedToBody.Pole)
        {
            return pole.AvailableElectricity > 0;
        }
        return false;
    }

    public void Connect(ElectricNode n)
    {
        other = n;
    }

    public void Disconect()
    {
        switch (body)
        {
            case AttachedToBody.Producer:
                break;
            case AttachedToBody.Pole:
                EchoReset();
                break;
            case AttachedToBody.Consumer:
                consumer.ShutDown();
                break;
            default:
                break;
        }
        other = null;
    }
}
