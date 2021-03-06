﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHelper : MonoBehaviour {

	static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        return findTransformWithName(ts, withName);
    }

    static public GameObject getParentGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInParent<Transform>();
        return findTransformWithName(ts, withName);
    }

    static public Vector3 getSizeFromRenderer(GameObject fromGameObject)
    {
        Quaternion initialRotation = fromGameObject.transform.rotation;
        fromGameObject.transform.rotation = Quaternion.identity;

        Renderer renderer = fromGameObject.GetComponent<Renderer>();
        if (!renderer) return Vector3.zero;
        Vector3 size = renderer.bounds.size;

        fromGameObject.transform.rotation = initialRotation;
        return size;
    }

    static private GameObject findTransformWithName(Transform[] transforms, string withName)
    {
        foreach (Transform t in transforms) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}
