﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojio.Events
{
    /// <summary>
    /// 
    /// </summary>
    [CollectionNameAttribute(typeof(Event))]
    public class ParkEvent : TripEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParkEvent"/> class.
        /// </summary>
        public ParkEvent() : base(EventType.Park)
        {
        }
    }
}
