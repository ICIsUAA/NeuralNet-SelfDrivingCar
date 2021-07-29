﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetDrawer : MonoBehaviour
{
    public float xOffset;
    public float yOffset;

    NeuralNetwork neuralNetwork;

    bool neuronsCreated = false;

    List<GameObject> inputNodes;
    List<GameObject> hiddenNodes;
    List<GameObject> outputNodes;
    Dictionary<string, GameObject> nodeMapping;

    // Start is called before the first frame update
    void Start()
    {
        inputNodes = new List<GameObject>();
        hiddenNodes = new List<GameObject>();
        outputNodes = new List<GameObject>();
        nodeMapping = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (neuralNetwork != null)
        {
            DrawNeuralNet();
        }
    }

    void DrawNeuralNet()
    {
        List<Neuron> inputs = neuralNetwork.inputList;
        List<Neuron> outputs = neuralNetwork.outputList;
        Neuron[,] layerNeurons = neuralNetwork.layerNeurons;


        if (!neuronsCreated)
        {
            CreateNodes(inputs, outputs);
        }

        // link nodes
        foreach (Neuron n in neuralNetwork.allNeurons)
        {
            var origin = nodeMapping[n.neuronId];
            foreach (Neuron nLink in n.outputList)
            {
                var destiny = nodeMapping[nLink.neuronId];
                Color color;
                if (n.output > .5f)
                    color = Color.red;
                else
                    color = Color.white;
                    
                Debug.DrawRay(nodeMapping[n.neuronId].transform.position, destiny.transform.localPosition - origin.transform.localPosition, color);
            }

            // print value

        }

    }

    private void CreateNodes(List<Neuron> inputs, List<Neuron> outputs)
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            var go = CreateSphere(new Vector3(0, i * yOffset, 0), "Input" + i);
            nodeMapping[inputs[i].neuronId] = go;
            inputNodes.Add(go);
        }

        for (int i = 0; i < neuralNetwork.hiddenLayers; i++)
        {
            for (int j = 0; j < neuralNetwork.nodesInHiddenLayers; j++)
            {
                var go = CreateSphere(new Vector3((1 + i) * xOffset, j * yOffset, 0), "Hidden" + i + "," + j);
                nodeMapping[neuralNetwork.layerNeurons[i,j].neuronId] = go;
                hiddenNodes.Add(go);
            }
        }


        for (int i = 0; i < outputs.Count; i++)
        {
            var go = CreateSphere(new Vector3((neuralNetwork.hiddenLayers + 1) * xOffset, i * yOffset, 0), "Output" + i);
            nodeMapping[outputs[i].neuronId] = go;
            outputNodes.Add(go);
        }

        neuronsCreated = true;
    }


    GameObject CreateSphere(Vector3 position, string name)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.parent = gameObject.transform;
        go.transform.localPosition = position;
        go.name = name;
        return go;
    }

    public void SetNeuralNetwork(NeuralNetwork neuralNetwork)
    {
        this.neuralNetwork = neuralNetwork;
    }
}
