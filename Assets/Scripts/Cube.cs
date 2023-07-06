using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoPooledObject
{
    public Color CurrentColor => _material.color;

    private Material _material;
    private Color _originalColor;

    protected override void OnCreate()
    {
        var meshRender = GetComponent<MeshRenderer>();
        _material = meshRender.material;
        _originalColor = meshRender.sharedMaterial.color;
    }

    public void PaintColour(Color color)
    {
        _material.color = color;
    }

    private void OnDisable()
    {
        PaintColour(_originalColor);
    }
}
