using System;

namespace RPAStudio.Actions
{
    public class MouseClickAction : RpaAction
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MouseClickAction(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override void Execute()
        {
            Console.WriteLine($"Executing Mouse Click at ({X}, {Y})");
            // TODO: Implement actual mouse click logic using platform-specific APIs
        }
    }
}