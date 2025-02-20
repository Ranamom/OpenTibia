﻿using OpenTibia.Common.Objects;

namespace OpenTibia.Game.Components
{
    public abstract class Behaviour : Component
    {
        public Context Context
        {
            get
            {
                return Context.Current;
            }
        }

        public virtual bool IsUnique
        {
            get
            {
                return true;
            }
        }

        public abstract void Start(Server server);

        public abstract void Stop(Server server);
    }
}