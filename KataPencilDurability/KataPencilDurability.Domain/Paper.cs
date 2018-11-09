namespace KataPencilDurability.Domain
{
    public class Paper
    {
        public string Text { get; internal set; }

        public Paper(string paperText = "")
        {
            Text = paperText;
        }
    }
}
