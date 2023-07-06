using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private int _commandCapacity;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private GameField _gameField;

    [Header("UI")]
    [SerializeField] private Button _undoButton;
    [SerializeField] private Button _redoButton;

    private CommandStackHanlder _commandStackHanlder;
    private CubeCommandHandler _cubeCommandHandler;

    public void Start()
    {
        _commandStackHanlder = new CommandStackHanlder(_commandCapacity);
        _cubeCommandHandler = new CubeCommandHandler(_cubePrefab, _gameField);

        _cubeCommandHandler.CubeSpawned += _commandStackHanlder.AddUndo;
        _cubeCommandHandler.CubeColored += _commandStackHanlder.AddUndo;

        _undoButton.onClick.AddListener(_commandStackHanlder.Undo);
        _redoButton.onClick.AddListener(_commandStackHanlder.ReDo);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _cubeCommandHandler.TrySpawnCube();
            return;
        }

        if(Input.GetMouseButtonDown(1))
        {
            _cubeCommandHandler.TryPaintCube();
            return;
        }
    }
}
