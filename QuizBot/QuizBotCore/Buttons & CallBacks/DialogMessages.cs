namespace QuizBotCore
{
    public static class DialogMessages
    {
        public const string CheckTaskCorrect = "А ты прав!";
        public const string CheckTaskWrong = "Подумай еще.";
        public const string FeedbackMessage = "Есть вопрос? Пиши нам!";
        public static readonly (string, string) FeedbackContact = ("Антон", "telegram.me/funfine");
        public const string NextTaskNotAvailable = "Реши эту, а потом подумаем о следующей";

        public const string SelectLevelMessage = "Вижу с темой ты определился. " +
                                                 "Выбирай уровень:";

        public static readonly char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public const string ProgressMessage = "Прогресс:";
        public const string ClosedLevel = "[Заблокирован]";
        public const string TopicName = "Тема:";
        public const string LevelName = "Уровень:";
        public const char ProgressFilled = '⬤';
        public const char ProgressEmpty = '◯';
        public const string NoHintsMessage = "Подсказок нет";
        public const string WelcomeMessage = "Привет! Я Quibble бот, представляю из себя бесконечную викторину. \n" +
                                             "Решай задачки, открывай новые уровни, становись лучше. \n" +
                                             "Умею в несколько тем. Выбирай тему и начинай!";
        
        public static readonly string LevelCompleted = "Ты решил все задачки из этого уровня👌🏿\n" +
                                                       $"Чтобы продолжить, - нажми \"{ButtonNames.Back}\" и выбери новый уровень.\n" +
                                                       "Или же можешь продолжить решать этот уровень.";
    }
}