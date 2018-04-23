using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Foundation[] _foundationStacks;
    private readonly List<Stack> _allStacks = new List<Stack>();

    private Stack _takenStack;
    private readonly List<Card> _takenCards = new List<Card>();

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
    private Transform[] _foundationRoots;

    [SerializeField]
    private Transform[] _tableRoots;
    #endregion

    private void Start()
    {
        Cursor.visible = false;
        _interaction.OnCardViewClicked += OnCardClicked;
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

        _foundationStacks = new Foundation[4];
        for (int i = 0; i < 4; i++)
        {
            _foundationStacks[i] = new Foundation(_foundationRoots[i]);
        }

        _allStacks.AddRange(_tableStacks);
        _allStacks.AddRange(_foundationStacks);
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

    private void OnCardClicked(Card card)
    {
        var ownerStack = _allStacks.Find(x => x.OwnsCard(card));
        if (ownerStack != null)
        {
            if (_takenCards.Count > 0)
            {
                foreach (var c in _takenCards)
                {
                    c.transform.SetParent(null);
                }

                var succ = ownerStack.PutCard(_takenCards);
                if (succ)
                {
                    _interaction.PlayDropSound();
                    foreach (var c in _takenCards)
                    {
                        c.transform.localScale = Vector3.one;
                    }

                    _takenCards.Clear();
                    _takenStack = null;
                }
                else
                {
                    foreach (var c in _takenCards)
                    {
                        c.transform.SetParent(_interaction.CardRoot);  
                    }
                }
            }
            else
            {
                _takenCards.Clear();
                _takenCards.AddRange(ownerStack.TakeCard(card));

                SetTakenCardVisuals(_takenCards);
                if (_takenCards.Count > 0)
                {
                    _interaction.PlayTakeSound();
                    _takenStack = ownerStack;
                    foreach (var takenCard in _takenCards)
                    {
                        takenCard.transform.SetParent(_interaction.CardRoot);
                        takenCard.transform.localRotation = Quaternion.identity;
                        takenCard.transform.localScale = Vector3.one;
                    }
                }
            }
        }
    }

    private void OnCardUndo()
    {
        if (_takenCards.Count == 0)
        {
            return;
        }

        _interaction.PlayDropSound();
        foreach (var takenCard in _takenCards)
        {
            takenCard.transform.SetParent(null);
            takenCard.transform.localScale = Vector3.one;
        }
        _takenStack.UndoCardTake(_takenCards);

        _takenCards.Clear();
        _takenStack = null;
    }

    private void SetTakenCardVisuals(List<Card> cards)
    {
        for (var i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            card.transform.SetParent(_interaction.CardRoot);
            card.transform.localPosition = Vector3.down * i * 0.3f;
            card.transform.localRotation = Quaternion.identity;
            card.transform.localScale = Vector3.one;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
