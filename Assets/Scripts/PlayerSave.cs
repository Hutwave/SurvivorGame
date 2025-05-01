using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
public class PlayerSave : ScriptableObject
{
    PlayerClass Class;
    List<Item> Items;
    List<Item> Equips;
    List<Skill> Skills;
    int Mesos;
    int Level;
    int Experience;
    int Str;
    int Int;
    int Luk;
}
