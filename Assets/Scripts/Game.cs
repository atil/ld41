using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Game : MonoBehaviour
{
    private readonly List<CardData> _allCardData = new List<CardData>();
    private Table[] _tableStacks;

    [SerializeField]
    private GameObject _cardPrefab;

    #region Roots
    [Header("CardData Stack Roots")]
    [SerializeField]
    private Transform _deckRoot;

    [SerializeField]
    private Transform _wastepileRoot;

    [SerializeField]
    private Transform _foundationRoot1;

    [SerializeField]
    private Transform _foundationRoot2;

    [SerializeField]
    private Transform _foundationRoot3;

    [SerializeField]
    private Transform _foundationRoot4;

    [SerializeField]
    private Transform[] _tableRoots;
    #endregion

    private void Start()
    {
        AddAllCardsOfType(CardType.Club);
        AddAllCardsOfType(CardType.Diamond);
        AddAllCardsOfType(CardType.Heart);
        AddAllCardsOfType(CardType.Spade);
        
        _tableStacks = new Table[7];

        var globalCardCounter = 0;
        for (int i = 0; i < 7; i++)
        {
            _tableStacks[i] = new Table(_tableRoots[i]);
            for (int j = 0; j < i; j++)
            {
                var card = Instantiate(_cardPrefab, _tableRoots[i]).GetComponent<Card>();
                card.SetWith(_allCardData[globalCardCounter]);
                _tableStacks[i].Add(card);

                globalCardCounter++;
            }

            _tableStacks[i].RefreshVisual();
        }
    }

    private void AddAllCardsOfType(CardType type)
    {
        for (int i = 1; i < 14; i++)
        {
            _allCardData.Add(new CardData()
            {
                Type = type,
                Number = i
            });
        }
    }
}
