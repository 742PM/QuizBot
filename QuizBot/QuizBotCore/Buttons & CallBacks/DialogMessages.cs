namespace QuizBotCore
{
    public static class DialogMessages
    {
        public const string AboutMessage = " ***Quibble*** происходит от двух фундаментальных слов:\n" +
                                           "*Quiz* + *Bible* - задаем вопросы, обучаем и даем ответы...\n" +
                                           "Обучение новому материалу проходит в виде бесконечной викторины.\n" +
                                           "Пройти уровень можно только решив определенное количество задач подряд.\n" +
                                           "*Желаем удачи!* ";

        public const string CheckTask_Correct = "А ты прав!";
        public const string CheckTask_Wrong = "Подумай еще.";
        public const string FeedbackMessage = "Есть вопрос? Пиши нам!";
        public static readonly (string, string) FeedbackContact = ("Антон", "telegram.me/funfine");
        public const string NextTask_NotAvailable = "Реши эту, а потом подумаем о следующей";

        public const string SelectLevelMessage = "Вижу с темой ты определился. " +
                                                 "Выбирай уровень:";

        public const string SelectTopicMessage = "Выбирай тему и погнали!";
        public const string ProgressMessage = "Прогресс:\n";
        public const char Progress_Filled = '⬤';
        public const char Progress_Empty = '◯';
        public const string NoHintsMessage = "Подсказок нет";
        public const string WelcomeMessage = "Главное меню:";
    }
}