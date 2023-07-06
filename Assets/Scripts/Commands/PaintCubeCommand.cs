using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCubeCommand : Command
{
    private Cube _cube;
    private Color _previous;
    private Color _current;

    public PaintCubeCommand(Cube cube)
    {
        _cube = cube;
        _previous = cube.CurrentColor;
        _current = GetRandomColor();
    }

    public override void Execute()
    {
        _cube.PaintColour(_current);
    }

    public override void Undo()
    {
        _cube.PaintColour(_previous);
    }

    private Color GetRandomColor()
    {
        var randomR = Random.Range(0.0f, 1.0f);
        var randomG = Random.Range(0.0f, 1.0f);
        var randomB = Random.Range(0.0f, 1.0f);

        return new Color(randomR, randomG, randomB);
    }
}
