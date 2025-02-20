﻿namespace OpenTibia.Game.Scripts
{
    public abstract class Script
    {
        public Context Context
        {
            get
            {
                return Context.Current;
            }
        }

        public abstract void Start(Server server);

        public abstract void Stop(Server server);
    }
}