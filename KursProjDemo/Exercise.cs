using System;
using System.Xml.Serialization;

namespace KursProjDemo
{
    public class Exercise
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("Image")]
        public string Image { get; set; }
        [XmlElement("Duration")]
        public int Duration { get; set; }
        public bool IsCompleted { get; set; }
    }
    public class ExerciseRecord
    {
        public string MethodName { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}