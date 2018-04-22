using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static GameObject CardPrefab
    {
        get { return Resources.Load<GameObject>("Prefabs/Card"); }
    }

    private readonly List<Card> _allCardData = new List<Card>();
    private Table[] _tableStacks;
    private Deck _deckStack;
    private Wastepile _wastepileStack;
    private readonly List<Stack> _allStacks = new List<Stack>();

    private Stack _takenStack;
    private Card _takenCard;

    private GameObject _cardPrefab;

    [SerializeField]
    private Interaction _interaction;

    #region Roots
    [Header("Card Stack Roots")]
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
        _interaction.OnCardViewClicked += OnCardViewClicked;
        _interaction.OnCardUndo += OnCardUndo;

        _cardPrefab = CardPrefab;
        AddAllCardsOfType(CardType.Club);
        AddAllCardsOfType(CardType.Diamond);
        AddAllCardsOfType(CardType.Heart);
        AddAllCardsOfType(CardType.Spade);
        _allCardData.Shuffle();

        _tableStacks = new Table[7];

        var globalCardCounter = 0;
        for (int i = 0; i < 7; i++)
        {
            _tableStacks[i] = new Table(_tableRoots[i]);
            for (int j = 0; j < i + 1; j++)
            {
                _tableStacks[i].Add(_allCardData[globalCardCounter]);

                globalCardCounter++;
            }

            _tableStacks[i].RefreshVisual(true);
        }

        _wastepileStack = new Wastepile(_wastepileRoot);
        _deckStack = new Deck(_deckRoot, _wastepileStack);

        for (int i = globalCardCounter; i < _allCardData.Count; i++)
        {
            _deckStack.Add(_allCardData[i]);
        }

        _deckStack.RefreshVisual(true);

        _allStacks.AddRange(_tableStacks);
        _allStacks.Add(_deckStack);
        _allStacks.Add(_wastepileStack);
    }

    private void AddAllCardsOfType(CardType type)
    {
        for (int i = 1; i < 14; i++)
        {
            var card = GameObject.Instantiate(_cardPrefab).GetComponent<Card>();
            card.SetData(type, i);
            _allCardData.Add(card);
        }
    }

    private void OnCardViewClicked(Card card)
    {
        var ownerStack = _allStacks.Find(x => x.OwnsCard(card));
        if (ownerStack != null)
        {
            if (_takenCard != null)
            {
                var tempRoot = _takenCard.transform.parent;
                _takenCard.transform.SetParent(null);

                var succ = ownerStack.PutCard(_takenCard);
                if (succ)
                {
                    _takenCard.transform.localScale = Vector3.one;
                    _takenCard = null;
                    _takenStack = null;
                }
                else
                {
                  _takenCard.transform.SetParent(tempRoot);  
                }
            }
            else
            {
                _takenCard = ownerStack.TakeCard();
                if (_takenCard != null)
                {
                    _takenCard.transform.SetParent(_interaction.CardRoot);
                    _takenCard.transform.localPosition = Vector3.zero;
                    _takenCard.transform.localRotation = Quaternion.identity;
                    _takenCard.transform.localScale = Vector3.one;
                    _takenStack = ownerStack;
                }
            }
        }
    }

    private void OnCardUndo()
    {
        if (_takenCard == null)
        {
            return;
        }
        _takenCard.transform.SetParent(null);
        _takenCard.transform.localScale = Vector3.one;

        _takenStack.UndoCardTake(_takenCard);

        _takenCard = null;
        _takenStack = null;
    }


}
