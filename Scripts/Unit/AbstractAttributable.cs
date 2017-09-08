using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttributable<T> : MonoBehaviour where T : struct, IAttribute
{

    public T Attributes => AttributesField;
    [SerializeField]
    protected T AttributesField;

    public abstract Dictionary<string, int> GetAttributes();

}
