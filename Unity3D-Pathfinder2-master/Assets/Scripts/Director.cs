using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GlobalPathFinder _globalPathFinder;
    [SerializeField] private LocalPathFinder _localPathFinder;

    [SerializeField] private Grid _grid;

    [SerializeField] private JumpRegion[] _jumpRegion;

    [SerializeField] private MinionMove _minion;
    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        _globalPathFinder.Setup();
        _grid.Setup();
        SearchJumpDots();
        StartCoroutine(DoCheck());
        StartCoroutine(StartMinion());
        // _globalPathFinder.SetupGlobalRegionInLocalPathFinder(_globalPathFinder.IDRegion);
        // _localPathFinder.SetupStartPosition(_globalPathFinder.SetupStartPositionInLocalPathFinder());
        // _localPathFinder.Setup();
    }


    IEnumerator DoCheck()
    {
        yield return new WaitForSeconds(1f);
         _globalPathFinder.SetupGlobalRegionInLocalPathFinder(_globalPathFinder.IDRegion);
        _localPathFinder.SetupStartPosition(_globalPathFinder.SetupStartPositionInLocalPathFinder());
        _localPathFinder.Setup();
    }
     IEnumerator StartMinion()
    {
        yield return new WaitForSeconds(3f);
         _minion.SetList();
    }
    private void SearchJumpDots()
    {
        foreach (JumpRegion jumpReg in _jumpRegion)
        {
            jumpReg.SearchDotsJump();
        }
    }


}
