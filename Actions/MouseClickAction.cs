using System;

namespace RPAStudio.Actions
{
    public class MouseClickAction : RpaAction
    {
        private static Random random = new Random();
        public int X { get; set; }
        public int Y { get; set; }

        public MouseClickAction(MouseClickActionParams parameters)
        {
            X = parameters.x;
            Y = parameters.y;
        }

        // Keep the original constructor for potential other uses or remove if not needed
        // public MouseClickAction(int x, int y)
        // {
        //     X = x;
        //     Y = y;
        // }

        public override void Execute()
        {
            int delay = random.Next(500, 2000); // Random delay between 500ms and 2000ms
            System.Threading.Thread.Sleep(delay);
            Console.WriteLine($"Executing Mouse Click at ({X}, {Y}) with delay {delay}ms");
            // TODO: Implement actual mouse click logic using platform-specific APIs
        }
    }
}