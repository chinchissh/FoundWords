namespace FiveLetters.BL.Models;

/// <summary>
/// Статус символа
/// </summary>
public enum LetterStatus
{
    NotGuessed, // Не угадано
    Wrong,      // В слове нет такой буквы
    Nearly,     // Угадано, но неправильная позиция
    Guessed     // Унадано
}
