﻿using OpenTibia.Common.Events;
using System;

namespace OpenTibia.Game.Commands
{
    public abstract class Command
    {
        public EventHandler<CommandCompletedEventArgs> Completed;

        public virtual void Execute(Context context)
        {
            if (Completed != null)
            {
                Completed(this, new CommandCompletedEventArgs(context) );
            }
        }
    }
}