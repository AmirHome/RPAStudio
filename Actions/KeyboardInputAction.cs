using System;

namespace RPAStudio.Actions
{
    public class KeyboardInput : RpaAction
    {
        private static Random random = new Random();
        public string Text { get; set; }

        public KeyboardInput(string text)
        {
            Text = text;
        }

        public override void Execute()
        {
            int delay = random.Next(500, 2000); // Random delay between 500ms and 2000ms
            System.Threading.Thread.Sleep(delay);
            Console.WriteLine($"Executing Keyboard Input: {Text} with delay {delay}ms");
            // TODO: Implement actual keyboard input logic using platform-specific APIs
        }
    }
}