﻿namespace Board.Entities
{
    public class Task : WorkItem
    {
        public string Activity { get; set; }
        public decimal RemaningWork { get; set; }
    }
}
