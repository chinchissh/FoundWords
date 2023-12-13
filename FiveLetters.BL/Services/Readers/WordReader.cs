using FiveLetters.BL.Models.Settings;

namespace FiveLetters.BL.Services.Readers;

/// <summary>
/// Класс для преобразования строк в слова с дополнительными проверками
/// </summary>
public sealed class WordReader
{
    public async Task<IReadOnlyList<string>> ReadWords(WordReaderSettings settings)
    {
        // Получить список слов из файла
        var words = await StringReader.ReadStrings(settings.FilePath);

        // Создать объект для проверки слов в соответствии с выбранным языком
        var wordChecker = WordCheckerFactory.CreateChecker(settings.LangMode);

        var processedWords = words
            .Select(x => x.Trim())
            .Where(x => wordChecker.CheckWord(x, settings.LettersCount))
            .Select(x => x.ToUpper())
            .ToList();

        // Вернуть список отфильтрованных слов
        return processedWords;
    }
}
