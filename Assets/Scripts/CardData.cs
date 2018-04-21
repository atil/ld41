using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum CardType
{
    None,
    Diamond,
    Heart,
    Spade,
    Club
}

public class CardData
{
    public CardType Type { get; set; }
    public int Number { get; set; }
}
