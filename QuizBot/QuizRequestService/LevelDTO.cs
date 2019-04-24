using System;

namespace QuizRequestService
{
    public class LevelDTO
    {
        public Guid Id { get; set; }
        public string Desctiption { get; set; }

        public LevelDTO(Guid id, string name)
        {
            Id = id;
            Desctiption = name;
        }
    }
}