namespace QuizRequestService
{
    public class HintDTO
    {
        public string HintText;
        public bool HasNext;

        public HintDTO(string hintText, bool hasNext)
        {
            HintText = hintText;
            HasNext = hasNext;
        }
    }
}