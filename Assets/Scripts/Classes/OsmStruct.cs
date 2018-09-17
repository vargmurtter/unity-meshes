using System;

[Serializable]
public class Way
{
    public string id;
    public Node[] nodes;
    public Tag[] tags;
}

[Serializable]
public class Node
{
    public string id;
    public float lon;
    public float lat;
}

[Serializable]
public class Tag
{
    public string key;
    public string value;
}