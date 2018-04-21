using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Game : MonoBehaviour
{
    private readonly List<CardModel> _allCardData = new List<CardModel>();
    private Table[] _tableStacks;
    private Deck _deckStack;
    private Wastepile _wastepile;

    [SerializeField]
    private GameObject _cardPrefab;

    #region Roots
    [Header("CardModel Stack Roots")]
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
                CreateCard(_allCardData[globalCardCounter], _tableRoots[i]);
                _tableStacks[i].Add(_allCardData[globalCardCounter]);

                globalCardCounter++;
            }

            _tableStacks[i].RefreshVisual();
        }

        _wastepile = new Wastepile(_wastepileRoot);
        _deckStack = new Deck(_deckRoot, _wastepile);

        for (int i = globalCardCounter; i < _allCardData.Count; i++)
        {
            CreateCard(_allCardData[i], _deckRoot);
            _deckStack.Add(_allCardData[i]);
        }

        _deckStack.RefreshVisual();
    }

    private void AddAllCardsOfType(CardType type)
    {
        for (int i = 1; i < 14; i++)
        {
            _allCardData.Add(new CardModel()
            {
                Type = type,
                Number = i
            });
        }
    }

    private CardView CreateCard(CardModel cardModel, Transform root)
    {
        var cardView = Instantiate(_cardPrefab, root).GetComponent<CardView>();
        cardView.SetWith(cardModel);

        return cardView;
    }
}
