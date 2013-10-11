﻿namespace NServiceBus.Pipeline.Behaviors
{
    using MessageMutator;
    using ObjectBuilder;

    public class ApplyIncomingMessageMutatorsBehavior : IBehavior
    {
        public IBehavior Next { get; set; }

        public IBuilder Builder { get; set; }

        public void Invoke(IBehaviorContext context)
        {
            var mutators = Builder.BuildAll<IMutateIncomingTransportMessages>();

            foreach (var mutator in mutators)
            {
                context.Trace("Applying transport message mutator {0}", mutator);
                mutator.MutateIncoming(context.TransportMessage);
            }

            Next.Invoke(context);
        }
    }
}