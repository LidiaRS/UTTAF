using System;
using System.Windows.Media;

namespace UTTAF.Desktop.Models
{
    public class MainModel
    {
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public string ResumeDescription { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}