﻿using FiveLetters.UI.Models;
using System.Threading.Tasks;

namespace FiveLetters.UI.Services.Interfaces;
internal interface IGameProcessor : IGameState
{
    void AddLetter(char letter);
    AttemptStatus CheckWord();
    bool NextWord();
    Task ReloadWords(Settings settings);
    void RemoveLetter();
}