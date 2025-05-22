using System.Collections.Generic;

namespace RPAStudio
{
    public class RpaWorkflow
    {
        public List<RpaAction> Actions { get; set; }

        public RpaWorkflow()
        {
            Actions = new List<RpaAction>();
        }

        public void AddAction(RpaAction action)
        {
            Actions.Add(action);
        }
    }
}