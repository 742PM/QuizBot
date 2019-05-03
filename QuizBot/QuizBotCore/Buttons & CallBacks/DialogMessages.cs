namespace QuizBotCore
{
    public static class DialogMessages
    {
        public const string AboutMessage = " ***Quibble*** происходит от двух фундаментальных слов:\n" +
                                           "*Quiz* + *Bible* - задаем вопросы, обучаем и даем ответы...\n" +
                                           "Обучение новому материалу проходит в виде бесконечной викторины.\n" +
                                           "Пройти уровень можно только решив определенное количество задач подряд.\n" +
                                           "*Желаем удачи!* ";

        public const string CheckTaskCorrect = "А ты прав!";
        public const string CheckTaskWrong = "Подумай еще.";
        public const string FeedbackMessage = "Есть вопрос? Пиши нам!";
        public static readonly (string, string) FeedbackContact = ("Антон", "telegram.me/funfine");
        public const string NextTaskNotAvailable = "Реши эту, а потом подумаем о следующей";

        public const string SelectLevelMessage = "Вижу с темой ты определился. " +
                                                 "Выбирай уровень:";

        public const string SelectTopicMessage = "Выбирай тему и погнали!";
        public const string ProgressMessage = "Прогресс:\n";
        public const char ProgressFilled = '⬤';
        public const char ProgressEmpty = '◯';
        public const string NoHintsMessage = "Подсказок нет";
        public const string WelcomeMessage = "Привет! Я Quibble бот, представляю из себя бесконечную викторину. \n" +
                                             "Решай задачки, открывай новые уровни, становись лучше. \n" +
                                             "Умею в несколько тем. Выбирай тему и начинай!";
        
        public static readonly string LevelCompleted = "Ты решил все задачки из этого уровня👌🏿\n" +
                                                       $"Чтобы продолжить, - нажми \"{ButtonNames.Back}\" и выбери новый уровень.\n" +
                                                       $"Чтобы взять еще задачку из этого уровня - жми \"{ButtonNames.NextTask}.\"";
    }
}