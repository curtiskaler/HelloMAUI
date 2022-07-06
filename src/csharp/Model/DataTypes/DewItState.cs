﻿using System.Diagnostics;
using DewIt.Model.Infrastructure;

namespace DewIt.Model.DataTypes
{
    public interface IDewItState : IApplicationState<DewItState>
    {
    }

    public class DewItState : IDewItState
    {
        private readonly IBootstrapper<DewItState> _bootstrapper;

        public LifeCycleState Status { get; set; } = LifeCycleState.UNINITIALIZED;

        public DewItState(IBootstrapper<DewItState> bootstrapper)
        {
            this._bootstrapper = bootstrapper;
        }


        public DewItState Initialize()
        {
            System.Diagnostics.Debug.WriteLine("DewItState: Initializing State!");

            try
            {
                this._bootstrapper.Bootstrap(this);
                this.Status = LifeCycleState.CREATED;
            }
            catch (Exception ex)
            {
                // Log the error and clean-up
                Debug.WriteLine(ex);
            }

            return this;
        }
    }
}