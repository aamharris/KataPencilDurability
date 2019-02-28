namespace KataPencilDurability.Domain
{
    public class Paper
    {
        public string Text { get; internal set; }
        public int LastEditedTextIndex { get; set; }

        public Paper(string paperText = "")
        {
            Text = paperText;
        }
    }
}
