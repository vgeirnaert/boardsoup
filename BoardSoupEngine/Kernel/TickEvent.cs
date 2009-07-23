namespace BoardSoupEngine.Kernel
{
    internal class TickEvent : KernelEvent
    {
        public TickEvent()
        {
        }

        public void execute(ITickable module)
        {
            module.onTick();
        }

        public override void execute(IEventListener module)
        {
            if (module is ITickable)
                this.execute((ITickable)module);
        }
    }
}
