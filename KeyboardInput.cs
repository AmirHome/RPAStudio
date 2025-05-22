using System;

namespace RPAStudio
{
    public class KeyboardInput : RpaAction
    {
        public string Text { get; set; }

        public KeyboardInput(string text)
        {
            Text = text;
        }

        public override void Execute()
        {
            Console.WriteLine($"Executing Keyboard Input: {Text}");
            // TODO: Implement actual keyboard input logic using platform-specific APIs
        }
    }
}