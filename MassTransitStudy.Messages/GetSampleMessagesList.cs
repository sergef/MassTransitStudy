﻿namespace MassTransitStudy.Messages
{
    using System;

    [Serializable]
    public class GetSampleMessagesList
    {
        public Guid CorrelationId
        {
            get;
            set;
        }

        public int StartIndex
        {
            get;
            set;
        }

        public int NumberOfItems
        {
            get;
            set;
        }
    }
}
