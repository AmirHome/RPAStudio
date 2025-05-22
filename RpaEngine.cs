using System;

namespace RPAStudio
{
    public class RpaEngine
    {
        public void ExecuteWorkflow(RpaWorkflow workflow)
        {
            Console.WriteLine("Starting RPA Workflow Execution...");
            foreach (var action in workflow.Actions)
            {
                action.Execute();
            }
            Console.WriteLine("RPA Workflow Execution Finished.");
        }
    }
}